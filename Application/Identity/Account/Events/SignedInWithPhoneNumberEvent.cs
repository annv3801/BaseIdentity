using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

// ReSharper disable All

namespace Application.Identity.Account.Events
{
    [ExcludeFromCodeCoverage]
    public class SignedInWithPhoneNumberEvent : INotification
    {
        public SignedInWithPhoneNumberEvent(Domain.Entities.Identity.Account account)
        {
            Account = account;
        }

        public Domain.Entities.Identity.Account Account { get; }
    }
}