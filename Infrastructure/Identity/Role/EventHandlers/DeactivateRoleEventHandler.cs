using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers;
[ExcludeFromCodeCoverage]
public class DeactivateRoleEventHandler : IDeactivateRoleEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public DeactivateRoleEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }

    public Task Handle(DeactivatedRoleEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
