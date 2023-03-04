using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class UpdateMyAccountEventHandler: IUpdateMyAccountEventHandler
{
    private readonly IJsonSerializerService _jsonSerializer;

    public UpdateMyAccountEventHandler(IJsonSerializerService jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }
    public Task Handle(UpdatedMyAccountEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
