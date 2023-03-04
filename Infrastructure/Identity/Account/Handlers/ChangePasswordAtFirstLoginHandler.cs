using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Account.Requests;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers;
[ExcludeFromCodeCoverage]
public class ChangePasswordAtFirstLoginHandler : IChangePasswordAtFirstLoginHandler
{
    private readonly IAccountManagementService _accountManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ChangePasswordAtFirstLoginHandler(IAccountManagementService accountManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _accountManagementService = accountManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<AccountResult>> Handle(ChangePasswordAtFirstLoginCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var request = _mapper.Map<ChangePasswordAtFirstLoginRequest>(command);
            return await _accountManagementService.ChangePasswordAtFirstLoginAsync(request, cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ChangePasswordAtFirstLoginHandler));
            Console.WriteLine(e);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
    }
}
