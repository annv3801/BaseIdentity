using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.ActionLog.Requests;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Common;
using Application.Identity.Permission.Queries;
using Application.Identity.Permission.Services;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Permission.Services;
public class PermissionManagementService : IPermissionManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizationService _localizationService;
    private readonly IActionLogService _actionLogService;
    private readonly ILoggerService _loggerService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public PermissionManagementService(IUnitOfWork unitOfWork, IStringLocalizationService localizationService, IActionLogService actionLogService, ILoggerService loggerService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _localizationService = localizationService;
        _actionLogService = actionLogService;
        _loggerService = loggerService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }

    public async Task<Result<PermissionResult>> UpdatePermissionAsync(Domain.Entities.Identity.Permission permission, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find object
            var perm = await _unitOfWork.Permissions.FindPermissionById(permission.Id, cancellationToken);
            if (perm == null)
                return Result<PermissionResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Copy data
            perm.Name = permission.Name;
            perm.Description = permission.Description;
            _unitOfWork.Permissions.Update(perm);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Permission.Update,
                    Message = LocalizationString.Permission.Updated,
                    MessageParams = new object[] {permission.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(permission)
                }, cancellationToken);
                return Result<PermissionResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Permission.Update,
                Message = LocalizationString.Permission.FailedToUpdate,
                MessageParams = new object[] {permission.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(permission)
            }, cancellationToken);
            return Result<PermissionResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Permission.Update,
                Message = LocalizationString.Permission.FailedToUpdate,
                MessageParams = new object[] {permission.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, nameof(UpdatePermissionAsync));
            throw;
        }
    }

    public async Task<Result<ViewPermissionResponse>> ViewPermissionAsync(Guid permId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Get permission
            var perm = await _unitOfWork.Permissions.FindPermissionByIdWithAuditableEntity(permId, cancellationToken);
            if (perm == null)
                return Result<ViewPermissionResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewPermissionResponse>.Succeed(data: _mapper.Map<ViewPermissionResponse>(perm));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewPermissionAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewPermissionResponse>>> ViewListPermissionsAsync(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var queryable = await _unitOfWork.Permissions.SearchPermissionByName(query, cancellationToken);
            var result = await _paginationService.PaginateAsync(queryable, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            return Result<PaginationBaseResponse<ViewPermissionResponse>>.Succeed(data: _mapper.Map<PaginationBaseResponse<ViewPermissionResponse>>(result));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListPermissionsAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}
