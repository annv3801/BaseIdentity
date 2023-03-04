using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using MediatR;

namespace Application.Identity.Account.Commands;
[ExcludeFromCodeCoverage]
public class SignInWithPhoneNumberCommand : IRequest<Result<SignInWithPhoneNumberResponse>>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? CaptchaToken { get; set; }
}
