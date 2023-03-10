using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class SignInWithUserNameCommand: IRequest<Result<SignInWithUserNameResponse>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? CaptchaToken { get; set; }
}