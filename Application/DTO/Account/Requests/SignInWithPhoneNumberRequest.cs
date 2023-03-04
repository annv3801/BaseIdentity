using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Requests;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberRequest
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}
