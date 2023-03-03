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
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class SetPasswordHandler : ISetPasswordHandler
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public SetPasswordHandler(IAccountManagementService accountManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _accountManagementService = accountManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<SetPasswordResponse>> Handle(SetPasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var request = _mapper.Map<SetPasswordRequest>(command);
                return await _accountManagementService.SetPassWordAsync(request, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(SetPasswordHandler));
                Console.WriteLine(e);
                return Result<SetPasswordResponse>.Fail(Constants.CommitFailed);
            }
        }
    }
}