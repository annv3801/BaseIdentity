using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;

#pragma warning disable 8618

namespace Application.DTO.Permission.Responses
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionResponse : AuditableView
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}