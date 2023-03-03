using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;
#pragma warning disable 8618

namespace Application.Identity.Account.Commands
{
    [ExcludeFromCodeCoverage]
    public class SetPasswordCommand : IRequest<Result<SetPasswordResponse>>
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool ForceAllSessionsLogout { get; set; }
    }
}