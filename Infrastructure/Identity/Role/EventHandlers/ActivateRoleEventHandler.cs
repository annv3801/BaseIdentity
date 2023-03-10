using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers;
[ExcludeFromCodeCoverage]
public class ActivateRoleEventHandler : IActivateRoleEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public ActivateRoleEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }

    public Task Handle(ActivatedRoleEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
