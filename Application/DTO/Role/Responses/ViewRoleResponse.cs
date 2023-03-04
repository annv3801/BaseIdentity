using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Permission.Responses;
using Domain.Enums;

namespace Application.DTO.Role.Responses;
[ExcludeFromCodeCoverage]
public class ViewRoleResponse : AuditableView
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string? Description { get; set; }
    public RoleStatus Status { get; set; }
    public List<ViewPermissionWithoutAuditResponse>? Permissions { get; set; }
}
