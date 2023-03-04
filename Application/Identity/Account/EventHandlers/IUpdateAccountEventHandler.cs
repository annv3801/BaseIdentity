﻿using Application.Identity.Account.Events;
using MediatR;

namespace Application.Identity.Account.EventHandlers;
public interface IUpdateAccountEventHandler: INotificationHandler<UpdatedAccountEvent>
{
    
}
