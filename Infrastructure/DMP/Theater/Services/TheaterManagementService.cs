using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Theater.Commons;
using Application.DMP.Theater.Queries;
using Application.DMP.Theater.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Theater.Services;
public class TheaterManagementService : ITheaterManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public TheaterManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<TheaterResult>> CreateTheaterAsync(Domain.Entities.DMP.Theater theater, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await _unitOfWork.Theaters.AddAsync(theater, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Theater.Create,
                    Message = LocalizationString.Theater.FailedToCreate,
                    MessageParams = new object[] {theater.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(theater)
                }, cancellationToken);
                return Result<TheaterResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Create,
                Message = LocalizationString.Theater.FailedToCreate,
                MessageParams = new object[] {theater.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(theater)
            }, cancellationToken);
            return Result<TheaterResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateTheaterAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Create,
                Message = LocalizationString.Theater.FailedToCreate,
                MessageParams = new object[] {theater.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewTheaterResponse>> ViewTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var theater = await _unitOfWork.Theaters.GetTheaterAsync(theaterId, cancellationToken);
            if (theater == null)
                return Result<ViewTheaterResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewTheaterResponse>.Succeed(data: _mapper.Map<ViewTheaterResponse>(theater));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewTheaterAsync));
            throw;
        }
    }
    public async Task<Result<TheaterResult>> DeleteTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find theater
            var theater = await _unitOfWork.Theaters.GetTheaterAsync(theaterId, cancellationToken);
            if (theater.Status == DMPStatus.Deleted)
                return Result<TheaterResult>.Fail(LocalizationString.Theater.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            theater.Status = DMPStatus.Deleted;
            _unitOfWork.Theaters.Update(theater);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Theater.Delete,
                    Message = LocalizationString.Theater.Deleted,
                    MessageParams = new object[] {theater.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(theater)
                }, cancellationToken);
                return Result<TheaterResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Delete,
                Message = LocalizationString.Theater.FailedToDelete,
                MessageParams = new object[] {theater.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(theater)
            }, cancellationToken);
            return Result<TheaterResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteTheaterAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Delete,
                Message = LocalizationString.Theater.FailedToDelete,
                MessageParams = new object[] {theaterId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<TheaterResult>> UpdateTheaterAsync(Domain.Entities.DMP.Theater theater, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var c = await _unitOfWork.Theaters.GetTheaterAsync(theater.Id, cancellationToken);
            if (c == null)
                return Result<TheaterResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = theater.Name;
            c.Status = theater.Status;
            c.Address = theater.Address;
            _unitOfWork.Theaters.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Theater.Update,
                    Message = LocalizationString.Theater.Updated,
                    MessageParams = new object[] {theater.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(theater)
                }, cancellationToken);
                return Result<TheaterResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Update,
                Message = LocalizationString.Theater.FailedToUpdate,
                MessageParams = new object[] {theater.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(theater)
            }, cancellationToken);
            return Result<TheaterResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateTheaterAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Theater.Update,
                Message = LocalizationString.Theater.FailedToUpdate,
                MessageParams = new object[] {theater.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewTheaterResponse>>> ViewListTheatersAsync(ViewListTheatersQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _unitOfWork.Theaters.ViewListTheatersAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Name != null && u.Name.ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.Name, p.Address, p.Status, p.Id});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewTheaterResponse>>.Fail(
                    _localizationService[LocalizationString.Theater.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewTheaterResponse>>.Succeed(new PaginationBaseResponse<ViewTheaterResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewTheaterResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListTheatersAsync));

            throw;
        }
    }
}
