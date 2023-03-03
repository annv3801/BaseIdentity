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
    public class LogoutEventHandler: ILogoutEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializerService;

        public LogoutEventHandler(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService;
        }

        public Task Handle(LoggedOutEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}