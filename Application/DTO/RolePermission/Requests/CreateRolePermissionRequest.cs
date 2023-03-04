using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.RolePermission.Requests;
[ExcludeFromCodeCoverage]
public class CreateRolePermissionRequest
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}
