using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Jwt.Responses;
[ExcludeFromCodeCoverage]
public class CreateJwtResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
