using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Account.Requests;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using AutoMapper;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class SignInWithPhoneNumberHandler : ISignInWithPhoneNumberHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountManagementService _accountManagementService;

        public SignInWithPhoneNumberHandler(IMapper mapper, IAccountManagementService accountManagementService)
        {
            _mapper = mapper;
            _accountManagementService = accountManagementService;
        }

        public async Task<Result<SignInWithPhoneNumberResponse>> Handle(SignInWithPhoneNumberCommand signInWithPhoneNumberCommand, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<SignInWithPhoneNumberRequest>(signInWithPhoneNumberCommand);

                var result = await _accountManagementService.SignInWithPasswordAsync(request, cancellationToken);
                return result.Succeeded
                    ? Result<SignInWithPhoneNumberResponse>.Succeed(result.Data)
                    : Result<SignInWithPhoneNumberResponse>.Fail(result.Errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result<SignInWithPhoneNumberResponse>.Fail(Constants.CommitFailed);
            }
        }
    }
}