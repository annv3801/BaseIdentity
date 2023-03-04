using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Responses;
[ExcludeFromCodeCoverage]
public class SignInWithUserNameResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}
