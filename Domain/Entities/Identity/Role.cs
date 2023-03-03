using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Domain.Common.Attributes.Sortable;
using Domain.Enums;

namespace Domain.Entities.Identity;
[ExcludeFromCodeCoverage]
public class Role : AuditableEntity
{
    public Guid Id { get; set; }
    [Sortable(OrderBy = "Name")] public string Name { get; set; }
    public string NormalizedName { get; set; }
    [Sortable(OrderBy = "Status")] public RoleStatus Status { get; set; }
    public string? Description { get; set; }
    public ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

}
