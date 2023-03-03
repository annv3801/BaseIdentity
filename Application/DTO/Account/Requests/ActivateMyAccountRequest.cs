using System.Diagnostics.CodeAnalysis;
#pragma warning disable 8618

namespace Application.DTO.Account.Requests
{
    [ExcludeFromCodeCoverage]
    public class ActivateMyAccountRequest
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
    }
}