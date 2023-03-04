using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.Identity.Account.EventHandlers;
using Application.Identity.Account.Events;

namespace Infrastructure.Identity.Account.EventHandlers;
[ExcludeFromCodeCoverage]
public class ChangePasswordAtFirstLoginEventHandler : IChangePasswordAtFirstLoginEventHandler
{
    private readonly IJsonSerializerService _jsonSerializerService;

    public ChangePasswordAtFirstLoginEventHandler(IJsonSerializerService jsonSerializerService)
    {
        _jsonSerializerService = jsonSerializerService;
    }
   
    public Task Handle(ChangedPasswordAtFirstLoginEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
