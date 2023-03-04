using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Account.Common;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class ForgotPasswordCommand: IRequest<Result<AccountResult>>
{
    public string PhoneNumber { get; set; }
    public string? CaptchaToken { get; set; }

}
