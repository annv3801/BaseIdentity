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
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class UpdateAccountHandler: IUpdateAccountHandler
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public UpdateAccountHandler(IAccountManagementService accountManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _accountManagementService = accountManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<AccountResult>> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var account = _mapper.Map<Domain.Entities.Identity.Account>(command);
                return await _accountManagementService.UpdateAccountAsync(account, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateAccountHandler));
                Console.WriteLine(e);
                return Result<AccountResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}