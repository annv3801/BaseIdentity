using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Account.Requests;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class ChangePasswordHandler : IChangePasswordHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILoggerService _loggerService;

        public ChangePasswordHandler(IMapper mapper, IAccountManagementService accountManagementService, ILoggerService loggerService)
        {
            _mapper = mapper;
            _accountManagementService = accountManagementService;
            _loggerService = loggerService;
        }

        public async Task<Result<AccountResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<ChangePasswordRequest>(command);
                return await _accountManagementService.ChangePasswordAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ChangePasswordHandler));
                Console.WriteLine(e);
                return Result<AccountResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}