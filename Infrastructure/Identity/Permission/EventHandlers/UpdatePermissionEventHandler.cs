using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Identity.Permission.EventHandlers;
using Application.Identity.Permission.Events;

namespace Infrastructure.Identity.Permission.EventHandlers
{
    [ExcludeFromCodeCoverage]
    public class UpdatePermissionEventHandler : IUpdatePermissionEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializerService;

        public UpdatePermissionEventHandler(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService;
        }

        public Task Handle(UpdatedPermissionEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}