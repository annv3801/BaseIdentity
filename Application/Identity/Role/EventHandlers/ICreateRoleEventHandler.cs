using Application.Common.Models;
using Application.Identity.Role.Events;
using MediatR;

namespace Application.Identity.Role.EventHandlers
{
    public interface ICreateRoleEventHandler : INotificationHandler<CreatedRoleEvent>
    {
    }
}