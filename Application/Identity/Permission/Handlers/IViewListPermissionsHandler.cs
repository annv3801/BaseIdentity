using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Queries;
using MediatR;

namespace Application.Identity.Permission.Handlers
{
    public interface IViewListPermissionsHandler : IRequestHandler<ViewListPermissionsQuery, Result<PaginationBaseResponse<ViewPermissionResponse>>>
    {
    }
}