using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers
{
    [ExcludeFromCodeCoverage]
    public class SignInWithPhoneNumberEventHandler : ISignInWithPhoneNumberEventHandler
    {
        public Task Handle(SignedInWithPhoneNumberEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}