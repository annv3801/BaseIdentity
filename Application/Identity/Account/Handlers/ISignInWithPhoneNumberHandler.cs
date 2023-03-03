using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Commands;
using MediatR;

namespace Application.Identity.Account.Handlers
{
    public interface ISignInWithPhoneNumberHandler : IRequestHandler<SignInWithPhoneNumberCommand, Result<SignInWithPhoneNumberResponse>>
    {
    }
}