﻿using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Account.Events
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class ChangedPasswordEvent : INotification
    {
        public ChangedPasswordEvent(Domain.Entities.Identity.Account account)
        {
            Account = account;
        }

        public Domain.Entities.Identity.Account Account { get; }
    }
}