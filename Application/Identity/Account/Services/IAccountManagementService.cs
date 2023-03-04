using Application.Common.Models;
using Application.DTO.Account.Requests;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Account.Common;

namespace Application.Identity.Account.Services;
public interface IAccountManagementService
{
    #region Admin

    Task<Result<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewAccountResponse>> ViewAccountDetailByAdminAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ActivateAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> DeactivateAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> LockAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> UnlockAccountAsync(Guid accountId,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SetPasswordResponse>> SetPassWordAsync(SetPasswordRequest request,CancellationToken cancellationToken = default(CancellationToken));

    Task<Result<PaginationBaseResponse<ViewAccountResponse>>> ViewListAccountsByAdminAsync(ViewListAccountsRequest request, CancellationToken cancellationToken = default(CancellationToken));

    #endregion

    #region Authorized Account

    Task<Result<AccountResult>> UpdateMyAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));

    #endregion

    #region Guest

    Task<Result<SignInWithPhoneNumberResponse>> SignInWithPasswordAsync(SignInWithPhoneNumberRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SignInWithUserNameResponse>> SignInWithUserNameAsync(SignInWithUserNameRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> LogOutAsync(bool forceEndOtherSessions=false,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ActivateMyAccountAsync(ActivateMyAccountRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> SetMyNewPasswordAsync(SetMyNewPasswordRequest request,CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<AccountResult>> ChangePasswordAtFirstLoginAsync(ChangePasswordAtFirstLoginRequest request, CancellationToken cancellationToken = default(CancellationToken));

    #endregion
}
