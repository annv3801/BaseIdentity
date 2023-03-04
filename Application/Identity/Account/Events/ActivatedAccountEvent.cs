using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace Application.Identity.Account.Events;
[ExcludeFromCodeCoverage]
public class ActivatedAccountEvent : INotification
{
    /// <inheritdoc />
    public ActivatedAccountEvent(Domain.Entities.Identity.Account account)
    {
        Account = account;
    }

    public Domain.Entities.Identity.Account Account { get; }
}
