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
    public class ActivateMyAccountEventHandler : IActivateMyAccountEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializer;

        public ActivateMyAccountEventHandler(IJsonSerializerService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public Task Handle(ActivatedMyAccountEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}