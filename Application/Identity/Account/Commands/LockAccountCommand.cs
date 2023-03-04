using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class LockAccountCommand : IRequest<Result<AccountResult>>
{
    public Guid Id { get; set; }
}
