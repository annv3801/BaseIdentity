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
    public class UpdateAccountEventHandler : IUpdateAccountEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializer;

        public UpdateAccountEventHandler(IJsonSerializerService jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        public Task Handle(UpdatedAccountEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}