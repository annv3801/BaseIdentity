using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using MediatR;

namespace Application.Identity.Permission.Events;
[ExcludeFromCodeCoverage]
public class UpdatedPermissionEvent : INotification
{
    /// <inheritdoc />
    public UpdatedPermissionEvent(Domain.Entities.Identity.Permission permission)
    {
        Permission = permission;
    }

    public Domain.Entities.Identity.Permission Permission { get; set; }
}
