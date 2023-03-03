using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.Account.Responses
{
    [ExcludeFromCodeCoverage]
    public class CreateAccountResponse
    {
        public string GeneratedPassword { get; set; }
    }
}