using Application.Identity.Permission.Events;
using MediatR;

namespace Application.Identity.Permission.EventHandlers;
public interface IUpdatePermissionEventHandler : INotificationHandler<UpdatedPermissionEvent>
{
}
