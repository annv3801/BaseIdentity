using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.ActionLog.Requests;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Commons;
using Application.Identity.Role.Events;
using Application.Identity.Role.Queries;
using Application.Identity.Role.Services;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Common;
using Domain.Enums;
using Domain.Interfaces;

namespace Infrastructure.Identity.Role.Services
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;
        private readonly IActionLogService _actionLogService;
        private readonly IStringLocalizationService _localizationService;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IMapper _mapper;
        private readonly IPaginationService _paginationService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loggerService"></param>
        /// <param name="actionLogService"></param>
        /// <param name="localizationService"></param>
        /// <param name="jsonSerializerService"></param>
        /// <param name="mapper"></param>
        /// <param name="paginationService"></param>
        public RoleManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
            _actionLogService = actionLogService;
            _localizationService = localizationService;
            _jsonSerializerService = jsonSerializerService;
            _mapper = mapper;
            _paginationService = paginationService;
        }


        /// <inheritdoc />
        public async Task<Result<RoleResult>> CreateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var existedRole = await _unitOfWork.Roles.GetRoleAsync(role.Name, cancellationToken);
                if (existedRole != null)
                    return Result<RoleResult>.Fail(LocalizationString.Role.Duplicated.ToErrors(_localizationService));
                await _unitOfWork.Roles.AddAsync(role, cancellationToken);
                var result = await _unitOfWork.CompleteAsync(cancellationToken);
                if (result > 0)
                {
                    await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                    {
                        Action = Constants.Actions.Identity.Role.Create,
                        Message = LocalizationString.Role.Created,
                        MessageParams = new object[] {role.Id.ToString()},
                        ExtraInfo = _jsonSerializerService.Serialize(role)
                    }, cancellationToken);
                    return Result<RoleResult>.Succeed();
                }

                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Create,
                    Message = LocalizationString.Role.FailedToCreate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(role)
                }, cancellationToken);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleAsync));
                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Create,
                    Message = LocalizationString.Role.FailedToCreate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = e.ToString()
                }, cancellationToken);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Result<RoleResult>> UpdateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var r = await _unitOfWork.Roles.GetRoleAsync(role.Id, cancellationToken);
                if (r == null)
                    return Result<RoleResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
                // Update data
                r.Description = role.Description;
                r.Name = role.Name;
                r.NormalizedName = role.Name.ToUpper();
                r.Status = role.Status;
                r.RolePermissions.Clear();
                r.RolePermissions = role.RolePermissions;
                _unitOfWork.Roles.Update(r);
                var result = await _unitOfWork.CompleteAsync(cancellationToken);
                if (result > 0)
                {
                    await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                    {
                        Action = Constants.Actions.Identity.Role.Update,
                        Message = LocalizationString.Role.Updated,
                        MessageParams = new object[] {role.Id.ToString()},
                        ExtraInfo = _jsonSerializerService.Serialize(role)
                    }, cancellationToken);
                    return Result<RoleResult>.Succeed();
                }

                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Update,
                    Message = LocalizationString.Role.FailedToUpdate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(role)
                }, cancellationToken);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleAsync));
                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Update,
                    Message = LocalizationString.Role.FailedToUpdate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = e.ToString()
                }, cancellationToken);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Result<ViewRoleResponse>> ViewRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _unitOfWork.Roles.GetRoleToViewDetail(roleId, cancellationToken);
                if (role == null)
                    return Result<ViewRoleResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
                return Result<ViewRoleResponse>.Succeed(data: _mapper.Map<ViewRoleResponse>(role));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleAsync));

                throw;
            }
        }

        public async Task<Result<PaginationBaseResponse<ViewRoleResponse>>> ViewListRolesAsync(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var queryable = await _unitOfWork.Roles.SearchRoleByName(query, cancellationToken);
                var result = await _paginationService.PaginateAsync(queryable, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
                return Result<PaginationBaseResponse<ViewRoleResponse>>.Succeed(data: _mapper.Map<PaginationBaseResponse<ViewRoleResponse>>(result));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleAsync));

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Result<RoleResult>> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _unitOfWork.Roles.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return Result<RoleResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
                if (role.Status == RoleStatus.Deleted)
                    return Result<RoleResult>.Fail(LocalizationString.Role.AlreadyDeleted.ToErrors(_localizationService));
                // Update data
                role.Status = RoleStatus.Deleted;
                _unitOfWork.Roles.Update(role);
                var result = await _unitOfWork.CompleteAsync(cancellationToken);
                if (result > 0)
                {
                    await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                    {
                        Action = Constants.Actions.Identity.Role.Delete,
                        Message = LocalizationString.Role.Deleted,
                        MessageParams = new object[] {role.Id.ToString()},
                        ExtraInfo = _jsonSerializerService.Serialize(role)
                    }, cancellationToken);
                    return Result<RoleResult>.Succeed();
                }

                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Delete,
                    Message = LocalizationString.Role.FailedToDelete,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(role)
                }, cancellationToken);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(DeleteRoleAsync));
                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Delete,
                    Message = LocalizationString.Role.FailedToDelete,
                    MessageParams = new object[] {roleId.ToString()},
                    ExtraInfo = e.ToString()
                }, cancellationToken);
                Console.WriteLine(e);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Result<RoleResult>> ActivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _unitOfWork.Roles.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return Result<RoleResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
                if (role.Status == RoleStatus.Active)
                    return Result<RoleResult>.Fail(LocalizationString.Role.AlreadyActivated.ToErrors(_localizationService));
                // Update data
                role.Status = RoleStatus.Active;
                _unitOfWork.Roles.Update(role);
                var result = await _unitOfWork.CompleteAsync(cancellationToken);
                if (result > 0)
                {
                    await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                    {
                        Action = Constants.Actions.Identity.Role.Activate,
                        Message = LocalizationString.Role.Activated,
                        MessageParams = new object[] {role.Id.ToString()},
                        ExtraInfo = _jsonSerializerService.Serialize(role)
                    }, cancellationToken);
                    return Result<RoleResult>.Succeed();
                }

                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Activate,
                    Message = LocalizationString.Role.FailedToActivate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(role)
                }, cancellationToken);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ActivateRoleAsync));
                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Activate,
                    Message = LocalizationString.Role.FailedToActivate,
                    MessageParams = new object[] {roleId.ToString()},
                    ExtraInfo = e.ToString()
                }, cancellationToken);
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Result<RoleResult>> DeactivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                // Find role
                var role = await _unitOfWork.Roles.GetRoleAsync(roleId, cancellationToken);
                if (role == null)
                    return Result<RoleResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
                if (role.Status == RoleStatus.Inactive)
                    return Result<RoleResult>.Fail(LocalizationString.Role.AlreadyDeactivated.ToErrors(_localizationService));
                // Update data
                role.Status = RoleStatus.Inactive;
                _unitOfWork.Roles.Update(role);
                var result = await _unitOfWork.CompleteAsync(cancellationToken);
                if (result > 0)
                {
                    await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                    {
                        Action = Constants.Actions.Identity.Role.Deactivate,
                        Message = LocalizationString.Role.Deactivated,
                        MessageParams = new object[] {role.Id.ToString()},
                        ExtraInfo = _jsonSerializerService.Serialize(role)
                    }, cancellationToken);
                    return Result<RoleResult>.Succeed();
                }

                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Deactivate,
                    Message = LocalizationString.Role.FailedToDeactivate,
                    MessageParams = new object[] {role.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(role)
                }, cancellationToken);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(DeactivateRoleAsync));
                await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Role.Deactivate,
                    Message = LocalizationString.Role.FailedToDeactivate,
                    MessageParams = new object[] {roleId.ToString()},
                    ExtraInfo = e.ToString()
                }, cancellationToken);
                Console.WriteLine(e);
                throw;
            }
        }
    }
}