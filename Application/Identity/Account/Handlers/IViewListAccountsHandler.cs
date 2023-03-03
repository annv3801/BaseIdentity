using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Account.Queries;
using MediatR;

namespace Application.Identity.Account.Handlers
{
    public interface IViewListAccountsHandler:IRequestHandler<ViewListAccountsQuery,Result<PaginationBaseResponse<ViewAccountResponse>>>
    {
        
    }
}