using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using MediatR;

namespace Application.Identity.Permission.Queries;
[ExcludeFromCodeCoverage]
public class ViewListPermissionsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewPermissionResponse>>>
{
}
