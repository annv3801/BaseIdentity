using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Category.Repositories;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Repositories;
using Application.DMP.Film.Services;
using Application.DMP.Slider.Commons;
using Application.DMP.Slider.Queries;
using Application.DMP.Slider.Repositories;
using Application.DMP.Slider.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Slider.Services;
public class SliderManagementService : ISliderManagementService
{
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISliderRepository _sliderRepository;
    private readonly IFileService _fileService;

    public SliderManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, IApplicationDbContext applicationDbContext, IFileService fileService, ISliderRepository sliderRepository)
    {
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
        _fileService = fileService;
        _sliderRepository = sliderRepository;
    }
    public async Task<Result<SliderResult>> CreateSliderAsync(Domain.Entities.DMP.Slider slider, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            if (slider.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(slider.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    slider.Image = fileResult.Item2; // getting name of image
                }
            }
            await _sliderRepository.AddAsync(slider, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Film.Create,
                    Message = LocalizationString.Film.FailedToCreate,
                    MessageParams = new object[] {slider.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(slider)
                }, cancellationToken);
                return Result<SliderResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {slider.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(slider)
            }, cancellationToken);
            return Result<SliderResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateSliderAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {slider.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewSliderResponse>> ViewSliderAsync(Guid sliderId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var slider = await _sliderRepository.GetSliderAsync(sliderId, cancellationToken);
            if (slider == null)
                return Result<ViewSliderResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewSliderResponse>.Succeed(new ViewSliderResponse()
            {
                Id = slider.Id,
                Image = slider.Image,
                Status = slider.Status
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewSliderAsync));
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewSliderResponse>>> ViewListSliderAsync(ViewListSlidersQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _sliderRepository.ViewListSlidersAsync(cancellationToken);
            var source = filterQuery.Select(p => new {p.Id, p.Image, p.Status});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSliderResponse>>.Fail(
                    _localizationService[LocalizationString.Film.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewSliderResponse>>.Succeed(new PaginationBaseResponse<ViewSliderResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewSliderResponse()
                {
                    Id = a.Id,
                    Image = a.Image,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSliderAsync));

            throw;
        }
    }
}
