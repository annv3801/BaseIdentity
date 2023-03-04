using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Role.Events;
[ExcludeFromCodeCoverage]
public class UpdatedRoleEvent : INotification
{
    public Domain.Entities.Identity.Role Role { get; set; }

    public UpdatedRoleEvent(Domain.Entities.Identity.Role role)
    {
        Role = role;
    }
}
