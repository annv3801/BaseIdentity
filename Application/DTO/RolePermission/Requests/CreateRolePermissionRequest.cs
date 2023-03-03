using System;
using System.Diagnostics.CodeAnalysis;
// ReSharper disable All

namespace Application.DTO.RolePermission.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateRolePermissionRequest
    {
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}