using Application.Identity.Account.Events;
using MediatR;

namespace Application.Identity.Account.EventHandlers;
public interface IDeactivateAccountEventHandler : INotificationHandler<DeactivatedAccountEvent>
{
}
