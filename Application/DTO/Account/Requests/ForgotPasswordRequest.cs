using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Requests;
[ExcludeFromCodeCoverage]
public class ForgotPasswordRequest
{
    public string PhoneNumber { get; set; }
}
