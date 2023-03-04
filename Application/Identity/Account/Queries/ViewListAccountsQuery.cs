using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.Identity.Account.Queries;
[ExcludeFromCodeCoverage]
public class ViewListAccountsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewAccountResponse>>>
{
}
