using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Domain.Common.Attributes.Sortable;
using Domain.Interfaces;

// ReSharper disable All
#pragma warning disable 8618

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class Permission : AuditableEntity
{
    public Guid Id { get; set; }
    [Sortable(OrderBy = "Name")] public string Name { get; set; }
    public string NormalizedName { get; set; }
    [Sortable(OrderBy = "Code")]
    public string Code { get; set; }
    public string? Description { get; set; }
    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();
}
