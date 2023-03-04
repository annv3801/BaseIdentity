using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace Application.Identity.Role.Events;
[ExcludeFromCodeCoverage]
public class ActivatedRoleEvent : INotification
{
    public ActivatedRoleEvent(Domain.Entities.Identity.Role role)
    {
        Role = role;
    }

    public Domain.Entities.Identity.Role Role { get; set; }
}
