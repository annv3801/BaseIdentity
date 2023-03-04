using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.Account.Responses;
[ExcludeFromCodeCoverage]
public class CreateAccountResponse
{
    public string GeneratedPassword { get; set; }
}
