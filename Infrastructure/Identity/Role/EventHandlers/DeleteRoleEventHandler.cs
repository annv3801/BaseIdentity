using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Events;

namespace Infrastructure.Identity.Role.EventHandlers;
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
