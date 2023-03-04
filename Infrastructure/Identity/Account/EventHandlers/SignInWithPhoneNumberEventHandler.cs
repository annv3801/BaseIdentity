using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberEventHandler : ISignInWithPhoneNumberEventHandler
{
    public Task Handle(SignedInWithPhoneNumberEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
