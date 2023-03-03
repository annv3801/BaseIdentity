using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class SignInWithPhoneNumberRequest
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}