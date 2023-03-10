using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Commons;
using Application.Identity.Role.Queries;

namespace Application.Identity.Role.Services;
public interface IRoleManagementService
{
    Task<Result<RoleResult>> CreateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoleResult>> UpdateRoleAsync(Domain.Entities.Identity.Role role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewRoleResponse>> ViewRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewRoleResponse>>> ViewListRolesAsync(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoleResult>> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoleResult>> ActivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoleResult>> DeactivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
}
