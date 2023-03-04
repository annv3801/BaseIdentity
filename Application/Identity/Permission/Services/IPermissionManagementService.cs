using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Common;
using Application.Identity.Permission.Queries;

namespace Application.Identity.Permission.Services;
public interface IPermissionManagementService
{
    Task<Result<PermissionResult>> UpdatePermissionAsync(Domain.Entities.Identity.Permission permission, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewPermissionResponse>> ViewPermissionAsync(Guid permId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewPermissionResponse>>> ViewListPermissionsAsync(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken));
}
