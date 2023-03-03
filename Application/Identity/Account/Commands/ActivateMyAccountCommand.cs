using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;
#pragma warning disable 8618

namespace Application.Identity.Account.Commands
{
    [ExcludeFromCodeCoverage]
    public class ActivateMyAccountCommand: IRequest<Result<AccountResult>>
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public string? CaptchaToken { get; set; }
    }
}