using System.Diagnostics.CodeAnalysis;
#pragma warning disable 8618

namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class SetPasswordRequest
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        
        public bool ForceAllSessionsLogout { get; set; }
    }
}