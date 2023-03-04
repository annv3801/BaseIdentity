using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Queries;
using MediatR;

namespace Application.Identity.Role.Handlers;
public interface IViewListRolesHandler: IRequestHandler<ViewListRolesQuery, Result<PaginationBaseResponse<ViewRoleResponse>>>
{
    
}
