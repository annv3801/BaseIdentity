using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers
{
    [ExcludeFromCodeCoverage]
    public class SignInWithUserNameEventHandler : ISingInWithUserNameEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializer;

        public SignInWithUserNameEventHandler(IJsonSerializerService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public Task Handle(SignedInWithUserNameEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}