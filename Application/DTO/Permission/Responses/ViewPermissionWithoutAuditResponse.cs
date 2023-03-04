using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Permission.Responses;
[ExcludeFromCodeCoverage]
public class ViewPermissionWithoutAuditResponse
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}
