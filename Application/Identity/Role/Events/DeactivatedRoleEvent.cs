using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Role.Events;
[ExcludeFromCodeCoverage]
public class DeactivatedRoleEvent : INotification
{
    public DeactivatedRoleEvent(Domain.Entities.Identity.Role role)
    {
        Role = role;
    }

    public Domain.Entities.Identity.Role Role { get; set; }
}
