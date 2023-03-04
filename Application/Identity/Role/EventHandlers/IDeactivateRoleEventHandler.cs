using Application.Identity.Role.Events;
using MediatR;

namespace Application.Identity.Role.EventHandlers;
public interface IDeactivateRoleEventHandler: INotificationHandler<DeactivatedRoleEvent>
{
    
}
