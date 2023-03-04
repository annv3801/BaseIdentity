using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Responses;
[ExcludeFromCodeCoverage]
public class SignInWithOAuthResponse
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Token { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}
