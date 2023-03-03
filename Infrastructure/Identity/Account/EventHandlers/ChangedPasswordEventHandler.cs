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
    public class ChangedPasswordEventHandler : IChangedPasswordEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializerService;

        public ChangedPasswordEventHandler(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService;
        }

        public Task Handle(ChangedPasswordEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}