using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers
{
    [ExcludeFromCodeCoverage]
    public class DeleteRoleEventHandler : IDeleteRoleEventHandler
    {
        private readonly IJsonSerializerService _jsonSerializerService;

        public DeleteRoleEventHandler(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService;
        }

        public Task Handle(DeletedRoleEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}