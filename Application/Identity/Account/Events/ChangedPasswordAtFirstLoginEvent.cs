﻿using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Account.Events;
[ExcludeFromCodeCoverage]
public class ChangedPasswordAtFirstLoginEvent: INotification
{
    /// <inheritdoc />
    public ChangedPasswordAtFirstLoginEvent(Domain.Entities.Identity.Account account)
    {
        Account = account;
    }

    public Domain.Entities.Identity.Account Account { get; }
}
