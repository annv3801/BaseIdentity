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
    public class SetMyNewPasswordHandler : ISetMyNewPasswordHandler
    {
        private readonly IMapper _mapper;
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILoggerService _loggerService;

        public SetMyNewPasswordHandler(IMapper mapper, IAccountManagementService accountManagementService, ILoggerService loggerService)
        {
            _mapper = mapper;
            _accountManagementService = accountManagementService;
            _loggerService = loggerService;
        }

        public async Task<Result<AccountResult>> Handle(SetMyNewPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<SetMyNewPasswordRequest>(command);
                return await _accountManagementService.SetMyNewPasswordAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(SetMyNewPasswordHandler));
                Console.WriteLine(e);
                return Result<AccountResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}