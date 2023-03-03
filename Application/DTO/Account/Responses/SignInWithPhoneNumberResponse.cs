using System.Diagnostics.CodeAnalysis;
// ReSharper disable All

#pragma warning disable 8618
namespace Application.DTO.Account.Responses
{
    [ExcludeFromCodeCoverage]
    public class SignInWithPhoneNumberResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}