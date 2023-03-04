using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class DeleteAccountEventHandler : IDeleteAccountEventHandler
{
    public Task Handle(DeletedAccountEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
