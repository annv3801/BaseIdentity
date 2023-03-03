using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;
#pragma warning disable 8618

namespace Application.Identity.Account.Commands
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class ChangePasswordCommand : IRequest<Result<AccountResult>>
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string ForceOtherSessionsLogout { get; set; }
        
    }
}