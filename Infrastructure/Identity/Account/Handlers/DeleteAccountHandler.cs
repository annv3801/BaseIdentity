using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Common;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Services;
using Domain.Interfaces;

namespace Infrastructure.Identity.Account.Handlers;
[ExcludeFromCodeCoverage]
public class DeleteAccountHandler : IDeleteAccountHandler
{
    private readonly IAccountManagementService _accountManagementService;
    private readonly ILoggerService _loggerService;

    public DeleteAccountHandler(IAccountManagementService accountManagementService,
        ILoggerService loggerService)
    {
        _accountManagementService = accountManagementService;
        _loggerService = loggerService;
    }

    public async Task<Result<AccountResult>> Handle(DeleteAccountCommand request,CancellationToken cancellationToken)
    {
        try
        {
            return await _accountManagementService.DeleteAccountAsync(request.AccountId, cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteAccountHandler));
            Console.WriteLine(e);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
    }
}
