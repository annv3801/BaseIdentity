using Application.Common.Models;
using Application.Identity.Account.Events;
using MediatR;

namespace Application.Identity.Account.EventHandlers
{
    public interface IUpdateMyAccountEventHandler : INotificationHandler<UpdatedMyAccountEvent>
    {
        
    }
}