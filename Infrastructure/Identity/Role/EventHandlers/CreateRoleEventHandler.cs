using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers;
[ExcludeFromCodeCoverage]
public class CreateRoleEventHandler : ICreateRoleEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public CreateRoleEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }

    public Task Handle(CreatedRoleEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
