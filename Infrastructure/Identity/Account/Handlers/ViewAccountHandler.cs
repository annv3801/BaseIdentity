using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Queries;
using Application.Identity.Account.Services;

namespace Infrastructure.Identity.Account.Handlers;
[ExcludeFromCodeCoverage]
public class ViewAccountHandler : IViewAccountHandler
{
    /// <summary>
    /// Inject the implementation of IUserManageService (from Infrastructure project)
    /// </summary>
    private readonly IAccountManagementService _accountManagementService;

    public ViewAccountHandler(IAccountManagementService accountManagementService)
    {
        _accountManagementService = accountManagementService;
    }
    
    public async Task<Result<ViewAccountResponse>> Handle(ViewAccountQuery request, CancellationToken cancellationToken)
    {
        return  await _accountManagementService.ViewAccountDetailByAdminAsync(request.AccountId, cancellationToken);
    }
}
