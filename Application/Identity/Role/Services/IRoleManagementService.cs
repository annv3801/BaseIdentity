using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Commons;
using Application.Identity.Role.Queries;

namespace Application.Identity.Role.Services
{
    public interface IRoleManagementService
    {
        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<RoleResult>> CreateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<RoleResult>> UpdateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<ViewRoleResponse>> ViewRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<PaginationBaseResponse<ViewRoleResponse>>> ViewListRolesAsync(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<RoleResult>> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Activate Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<RoleResult>> ActivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deactivate Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<RoleResult>> DeactivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    }
}