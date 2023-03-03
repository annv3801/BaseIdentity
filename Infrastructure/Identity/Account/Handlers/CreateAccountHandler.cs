using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using AutoMapper;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class CreateAccountHandler : ICreateAccountHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountManagementService _accountManagementService;
        private readonly IPasswordGeneratorService _passwordGeneratorService;

        public CreateAccountHandler(IMapper mapper, IAccountManagementService accountManagementService,
            IPasswordGeneratorService passwordGeneratorService)
        {
            _mapper = mapper;
            _accountManagementService = accountManagementService;
            _passwordGeneratorService = passwordGeneratorService;
        }

        public async Task<Result<CreateAccountResponse>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = _mapper.Map<Domain.Entities.Identity.Account>(request);
                // Generate password
                var password = _passwordGeneratorService.GenerateRandomPassword();
                account.PasswordHash = _passwordGeneratorService.HashPassword(password);
                account.PasswordHashTemporary = account.PasswordHash;
                // End
                var result = await _accountManagementService.CreateAccountByAdminAsync(account, cancellationToken);
                return result.Succeeded
                    ? Result<CreateAccountResponse>.Succeed(new CreateAccountResponse()
                        {GeneratedPassword = password})
                    : Result<CreateAccountResponse>.Fail(result.Errors);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Result<CreateAccountResponse>.Fail(Constants.CommitFailed);
            }
        }
    }
}