using Application.Common.Models;
using Application.Identity.Account.Events;
using MediatR;

namespace Application.Identity.Account.EventHandlers
{
    /// <inheritdoc />
    public interface IChangedPasswordEventHandler: INotificationHandler<ChangedPasswordEvent>
    {
        
    }
}