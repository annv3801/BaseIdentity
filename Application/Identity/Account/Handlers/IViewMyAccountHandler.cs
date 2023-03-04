using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Queries;
using MediatR;

namespace Application.Identity.Account.Handlers;
public interface IViewMyAccountHandler : IRequestHandler<ViewAccountQuery,Result<ViewAccountResponse>>
{
    
}
