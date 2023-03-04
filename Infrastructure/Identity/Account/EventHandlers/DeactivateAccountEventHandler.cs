﻿using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class DeactivateAccountEventHandler : IDeactivateAccountEventHandler
{
    public Task Handle(DeactivatedAccountEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
