using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeactivateAccountCommand : IRequest<Result<AccountResult>>
    {
        public Guid AccountId { get; set; }
    }
}