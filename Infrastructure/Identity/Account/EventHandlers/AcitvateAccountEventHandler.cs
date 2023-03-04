using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class ActivateAccountEventHandler : IActivateAccountEventHandler
{
    public Task Handle(ActivatedAccountEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
