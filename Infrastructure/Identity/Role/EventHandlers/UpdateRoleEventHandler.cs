using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers;
[ExcludeFromCodeCoverage]
public class UpdateRoleEventHandler : IUpdateRoleEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public UpdateRoleEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }

    public Task Handle(UpdatedRoleEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
