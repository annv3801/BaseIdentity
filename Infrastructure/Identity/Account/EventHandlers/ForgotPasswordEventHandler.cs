using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class ForgotPasswordEventHandler : IForgotPasswordEventHandler
{
    private readonly IJsonSerializerService _jsonSerializer;

    public ForgotPasswordEventHandler(IJsonSerializerService jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
