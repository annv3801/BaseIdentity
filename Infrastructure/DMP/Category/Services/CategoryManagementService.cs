using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DMP.Category.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Category.Responses;
using Application.Identity.Role.Commons;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.DMP.Category.Services;
public class CategoryManagementService : ICategoryManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public CategoryManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<CategoryResult>> CreateCategoryAsync(Domain.Entities.DMP.Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await _unitOfWork.Categories.AddAsync(category, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Category.Create,
                    Message = LocalizationString.Category.FailedToCreate,
                    MessageParams = new object[] {category.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(category)
                }, cancellationToken);
                return Result<CategoryResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Create,
                Message = LocalizationString.Category.FailedToCreate,
                MessageParams = new object[] {category.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(category)
            }, cancellationToken);
            return Result<CategoryResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateCategoryAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Create,
                Message = LocalizationString.Category.FailedToCreate,
                MessageParams = new object[] {category.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewCategoryResponse>> ViewCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var category = await _unitOfWork.Categories.GetCategoryAsync(categoryId, cancellationToken);
            if (category == null)
                return Result<ViewCategoryResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewCategoryResponse>.Succeed(data: _mapper.Map<ViewCategoryResponse>(category));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewCategoryAsync));
            throw;
        }
    }
    public async Task<Result<CategoryResult>> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find category
            var category = await _unitOfWork.Categories.GetCategoryAsync(categoryId, cancellationToken);
            if (category.Status == DMPStatus.Deleted)
                return Result<CategoryResult>.Fail(LocalizationString.Category.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            category.Status = DMPStatus.Deleted;
            _unitOfWork.Categories.Update(category);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Category.Delete,
                    Message = LocalizationString.Category.Deleted,
                    MessageParams = new object[] {category.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(category)
                }, cancellationToken);
                return Result<CategoryResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Delete,
                Message = LocalizationString.Category.FailedToDelete,
                MessageParams = new object[] {category.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(category)
            }, cancellationToken);
            return Result<CategoryResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteCategoryAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Delete,
                Message = LocalizationString.Category.FailedToDelete,
                MessageParams = new object[] {categoryId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<CategoryResult>> UpdateCategoryAsync(Domain.Entities.DMP.Category category, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var c = await _unitOfWork.Categories.GetCategoryAsync(category.Id, cancellationToken);
            if (c == null)
                return Result<CategoryResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = category.Name;
            c.Status = category.Status;
            c.ShortenUrl = category.ShortenUrl;
            _unitOfWork.Categories.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Category.Update,
                    Message = LocalizationString.Category.Updated,
                    MessageParams = new object[] {category.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(category)
                }, cancellationToken);
                return Result<CategoryResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Update,
                Message = LocalizationString.Category.FailedToUpdate,
                MessageParams = new object[] {category.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(category)
            }, cancellationToken);
            return Result<CategoryResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateCategoryAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Category.Update,
                Message = LocalizationString.Category.FailedToUpdate,
                MessageParams = new object[] {category.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    
}
