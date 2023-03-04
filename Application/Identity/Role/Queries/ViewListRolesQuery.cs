using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using MediatR;

namespace Application.Identity.Role.Queries;
[ExcludeFromCodeCoverage]
public class ViewListRolesQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewRoleResponse>>>
{
}
