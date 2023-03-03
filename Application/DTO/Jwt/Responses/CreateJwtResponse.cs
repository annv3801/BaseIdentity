using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.Jwt.Responses
{
    [ExcludeFromCodeCoverage]
    public class CreateJwtResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}