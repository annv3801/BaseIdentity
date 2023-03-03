using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.Role.Requests
{
    [ExcludeFromCodeCoverage]
    public class CreateRoleRequest
    {
        public string Name { get; set; } = "";
        public RoleStatus Status { get; set; } = RoleStatus.Active;
        public string? Description { get; set; } = null;
        public List<Guid> Permissions { get; set; } = new List<Guid>();
    }
}