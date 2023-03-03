using Application.Common.Models;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Handlers
{
    /// <inheritdoc />
    public interface IActivateAccountHandler : IRequestHandler<ActivateAccountCommand, Result<AccountResult>>
    {
    }
}