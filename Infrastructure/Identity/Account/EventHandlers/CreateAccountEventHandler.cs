using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class CreateAccountEventHandler : ICreateAccountEventHandler
{
    public Task Handle(CreatedAccountEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
