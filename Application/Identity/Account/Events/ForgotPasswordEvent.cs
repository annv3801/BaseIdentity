using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Account.Events
{
    [ExcludeFromCodeCoverage]
    public class ForgotPasswordEvent : INotification
    {
        public ForgotPasswordEvent(Domain.Entities.Identity.Account account)
        {
            Account = account;
        }

        public Domain.Entities.Identity.Account Account { get; }
    }
}