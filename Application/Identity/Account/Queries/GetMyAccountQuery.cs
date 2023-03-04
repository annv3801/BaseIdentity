using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;

namespace Application.Identity.Account.Queries;
[ExcludeFromCodeCoverage]
public class GetMyAccountQuery : IRequest<Result<ViewAccountResponse>>
{
    public Guid UserId { get; set; }
}
