using System;
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
    public class SignInWithUserNameHandler : ISignInWithUserNameHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountManagementService _accountManagementService;

        public SignInWithUserNameHandler(IAccountManagementService accountManagementService, IMapper mapper)
        {
            _accountManagementService = accountManagementService;
            _mapper = mapper;
        }

        public async Task<Result<SignInWithUserNameResponse>> Handle(SignInWithUserNameCommand signInWithUserNameCommand, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<SignInWithUserNameRequest>(signInWithUserNameCommand);

                var result = await _accountManagementService.SignInWithUserNameAsync(request, cancellationToken);
                return result.Succeeded ? Result<SignInWithUserNameResponse>.Succeed(result.Data) : Result<SignInWithUserNameResponse>.Fail(result.Errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result<SignInWithUserNameResponse>.Fail(Constants.CommitFailed);
            }
        }
    }
}