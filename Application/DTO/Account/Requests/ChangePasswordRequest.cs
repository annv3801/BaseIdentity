using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool ForceOtherSessionsLogout { get; set; }
        
    }
}