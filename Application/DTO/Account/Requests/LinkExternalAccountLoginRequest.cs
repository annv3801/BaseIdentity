using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Requests;
[ExcludeFromCodeCoverage]
public class LinkExternalAccountLoginRequest
{
    public string Id { get; set; } = "";// Provider key of Login Provider
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public bool? Gender { get; set; } = true;
    public string? AvatarPhoto { get; set; } = "";
    public List<Guid>? Roles { get; set; } = new List<Guid>();
}
