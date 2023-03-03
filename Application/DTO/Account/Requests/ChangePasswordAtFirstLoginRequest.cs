using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class ChangePasswordAtFirstLoginRequest
    {
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}