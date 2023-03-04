using Application.Common.Models;
using Application.DTO.Account.Requests;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Account.Common;

namespace Application.Identity.Account.Services;
public interface IAccountManagementService
{
    Task<Result<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewAccountResponse>> ViewAccountDetailByAdminAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UnlockAccountAsync(Guid accountId,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SignInWithUserNameResponse>> SignInWithUserNameAsync(SignInWithUserNameRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> LogOutAsync(bool forceEndOtherSessions=false,CancellationToken cancellationToken = default(CancellationToken));
}
