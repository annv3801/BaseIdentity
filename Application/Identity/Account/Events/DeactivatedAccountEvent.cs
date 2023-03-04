using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Account.Events;
[ExcludeFromCodeCoverage]
public class DeactivatedAccountEvent : INotification
{
    public DeactivatedAccountEvent(Domain.Entities.Identity.Account account)
    {
        Account = account;
    }

    public Domain.Entities.Identity.Account Account { get; }
}
