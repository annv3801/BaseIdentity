using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class DeactivateAccountHandler : IDeactivateAccountCommandHandler
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILoggerService _loggerService;

        public DeactivateAccountHandler(IAccountManagementService accountManagementService, ILoggerService loggerService)
        {
            _accountManagementService = accountManagementService;
            _loggerService = loggerService;
        }

        public async Task<Result<AccountResult>> Handle(DeactivateAccountCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                return  await _accountManagementService.DeactivateAccountAsync(request.AccountId, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(DeactivateAccountHandler));
                Console.WriteLine(e);
                return Result<AccountResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}