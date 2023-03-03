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
    public class UnlockAccountEventHandler : IUnlockAccountEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializer;

        public UnlockAccountEventHandler(IJsonSerializerService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public Task Handle(UnlockedAccountEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}