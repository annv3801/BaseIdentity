using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class ChangePasswordAtFirstLoginCommand: IRequest<Result<AccountResult>>
{
    public string UserName { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
