using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Requests;
[ExcludeFromCodeCoverage]
public class SignInWithUserNameRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
