using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable All
#pragma warning disable 8618

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class RolePermission
{
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }
}
