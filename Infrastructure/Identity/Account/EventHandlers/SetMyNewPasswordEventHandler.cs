using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class SetMyNewPasswordEventHandler : ISetMyNewPasswordEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public SetMyNewPasswordEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }

    public Task Handle(SetMyNewPasswordEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
