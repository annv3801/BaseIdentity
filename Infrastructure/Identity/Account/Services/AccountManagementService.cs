using System.Security.Claims;
using System.Text.Json;
using Application.Common;
using Application.Common.Configurations;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.ActionLog.Requests;
using Application.DTO.Account.Requests;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Account.Common;
using Application.Identity.Account.Services;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Constants;
using Domain.Entities.Identity;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Identity.Account.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Services;
public class AccountManagementService : IAccountManagementService
{
    private readonly IDateTime _dateTime;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IActionLogService _actionLogService;
    private readonly ApplicationConfiguration _appOption;
    private readonly IPasswordGeneratorService _passwordGeneratorService;
    private readonly IJwtService _jwtService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IPaginationService _paginationService;
    private readonly IMapper _mapper;
    private readonly ICurrentAccountService _currentAccountService;
    public AccountManagementService(IUnitOfWork unitOfWork, IDateTime dateTime,
        IStringLocalizationService localizationService, IActionLogService actionLogService,
        ILoggerService loggerService, IOptions<ApplicationConfiguration> appOption, IPasswordGeneratorService passwordGeneratorService,
        IJwtService jwtService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, ICurrentAccountService currentAccountService)
    {
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _localizationService = localizationService;
        _actionLogService = actionLogService;
        _loggerService = loggerService;
        _appOption = appOption.Value;
        _passwordGeneratorService = passwordGeneratorService;
        _jwtService = jwtService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _currentAccountService = currentAccountService;
    }

    #region Admin

    public async Task<Result<AccountResult>> CreateAccountByAdminAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var validator = new CreateAccountEntityValidator(_localizationService, _unitOfWork);
            var validation = await validator.ValidateAsync(account, cancellationToken);
            if (!validation.IsValid)
                return Result<AccountResult>.Fail(validation.Errors.BuildArray());
            var duplicatedEnumerable = await _unitOfWork.Accounts.FindAsync(x => x.NormalizedUserName == account.NormalizedUserName, cancellationToken);
            if (duplicatedEnumerable.Any())
            {
                return Result<AccountResult>.Fail(Constants.DuplicatedItem);
            }
            await _unitOfWork.Accounts.AddAsync(account, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(account, cancellationToken);
            if (result > 0)
            {
                // // Send OTP if enabled
                // if (_appOption.EnableRegistrationWithOtp)
                // {
                //     await _smsService.SendOtpAsync(account.PhoneNumber, account.Otp, cancellationToken);
                // }

                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Create,
                    Message = LocalizationString.Account.CreatedAccount,
                    MessageParams = new object[] {account.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(account)
                }, cancellationToken);
                return Result<AccountResult>.Succeed(new AccountResult());
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Create,
                Message = LocalizationString.Account.FailedToCreateAccount,
                MessageParams = new object[] {account.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(account)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Create,
                Message = LocalizationString.Account.FailedToCreateAccount,
                MessageParams = new object[] {account.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    public async Task<Result<AccountResult>> UpdateAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account
            var existedAccount = await _unitOfWork.Accounts.ViewAccountDetailByAdmin(account.Id, cancellationToken);

            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            //Check duplicated email 
            var emailCheck = await _unitOfWork.Accounts.IsDuplicatedEmailAsync(existedAccount.Id, existedAccount.Email, cancellationToken);
            if (emailCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value.ToErrors(_localizationService));

            //Check duplicated phone 
            var phoneCheck = await _unitOfWork.Accounts.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value.ToErrors(_localizationService));

            // Check current account permission
            // If account is sysadmin --> can update
            // If account is tenant admin-> can update user who belong his tenant only --> return forbid http status code
            var hasTenantAdmin = _currentAccountService.HasPerm(Constants.Permissions.TenantAdmin);

            // Tenant admin
            if (hasTenantAdmin)
                if (existedAccount.CreatedById != _currentAccountService.Id)
                    return Result<AccountResult>.Forbid(_localizationService[LocalizationString.Account.PermissionDenied].Value.ToErrors(_localizationService));

            _mapper.Map(account, existedAccount);

            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Update,
                    Message = LocalizationString.Account.UpdatedAccount,
                    MessageParams = new object[] {existedAccount.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
                }, cancellationToken);
                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Update,
                Message = LocalizationString.Account.FailedToUpdateAccount,
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Update,
                Message = LocalizationString.Account.FailedToUpdateAccount,
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    public async Task<Result<ViewAccountResponse>> ViewAccountDetailByAdminAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var existedAccount = await _unitOfWork.Accounts.ViewAccountDetailByAdmin(accountId, cancellationToken);
            if (existedAccount == null)
            {
                return Result<ViewAccountResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
            }

            await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.View,
                Message = LocalizationString.Account.ViewedAccountDetailByAdmin,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = JsonSerializer.Serialize(existedAccount, Constants.JsonSerializerOptions)
            }, cancellationToken);
            var accountDetailResponse = _mapper.Map<ViewAccountResponse>(existedAccount);

            return Result<ViewAccountResponse>.Succeed(accountDetailResponse);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.View,
                Message = LocalizationString.Account.FailedToViewAccountDetailByAdmin,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    public async Task<Result<AccountResult>> DeleteAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check user id
            var existedAccount = await _unitOfWork.Accounts.FindByIdAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value
                    .ToErrors(_localizationService));

            if (existedAccount.Status == AccountStatus.Deleted)
                return Result<AccountResult>.Fail(
                    _localizationService[LocalizationString.Account.AccountIsDeleted].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Deleted;

            // remove all jwt token to force user logout
            var listAccountTokens =
                await _unitOfWork.AccountTokens.GetAccountTokensByAccountAsync(accountId, cancellationToken);
            _unitOfWork.AccountTokens.RemoveRange(listAccountTokens);
            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0) // Enable Succeeded 
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Delete,
                    Message = LocalizationString.Account.DeletedAccount,
                    MessageParams = new object[] {accountId.ToString()},
                    ExtraInfo = JsonSerializer.Serialize(existedAccount, Constants.JsonSerializerOptions)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }


            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Delete,
                Message = LocalizationString.Account.FailedToDeleteAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = JsonSerializer.Serialize(existedAccount, Constants.JsonSerializerOptions)
            }, cancellationToken);

            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Delete,
                Message = LocalizationString.Account.FailedToDeleteAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<Result<AccountResult>> ActivateAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account id
            var existedAccount = await _unitOfWork.Accounts.FindByIdAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            // Check that user only can be enabled if its current status is inactive
            if (existedAccount.Status != AccountStatus.Inactive)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.AccountIsNotInActive].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Active;
            // Add event enable account

            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0) // Enable Succeeded 
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Activate,
                    Message = LocalizationString.Account.ActivatedAccount,
                    MessageParams = new object[] {accountId.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Activate,
                Message = LocalizationString.Account.FailedToActiveAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Activate,
                Message = LocalizationString.Account.FailedToActiveAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, nameof(ActivateAccountAsync));
            throw;
        }
    }

    public async Task<Result<AccountResult>> DeactivateAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check user id
            var existedAccount = await _unitOfWork.Accounts.FindByIdAsync(accountId, cancellationToken: cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            if (existedAccount.Status == AccountStatus.Inactive)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.AccountIsDeactivate].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Inactive;

            // remove all jwt token to force user logout
            var listAccountTokens = await _unitOfWork.AccountTokens.GetAccountTokensByAccountAsync(accountId, cancellationToken);
            _unitOfWork.AccountTokens.RemoveRange(listAccountTokens);
            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0) // Enable Succeeded 
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Deactivate,
                    Message = LocalizationString.Account.DeactivatedAccount,
                    MessageParams = new object[] {accountId.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Deactivate,
                Message = LocalizationString.Account.FailedToDeactivateAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Deactivate,
                Message = LocalizationString.Account.FailedToDeactivateAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    public async Task<Result<AccountResult>> LockAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account id
            var existedAccount = await _unitOfWork.Accounts.FindByIdAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            if (existedAccount.Status == AccountStatus.Locked)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.LockedYet].Value.ToErrors(_localizationService));

            // Check current account permission
            // If account is sysadmin --> can update
            // If account is tenant admin-> can update user who belong his tenant only --> return forbid http status code
            var hasTenantAdmin = _currentAccountService.HasPerm(Constants.Permissions.TenantAdmin);

            // Tenant admin
            if (hasTenantAdmin)
                if (existedAccount.CreatedById != _currentAccountService.Id)
                    return Result<AccountResult>.Forbid(_localizationService[LocalizationString.Account.PermissionDenied].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Locked;

            // remove all jwt token to force user logout
            var listAccountTokens = await _unitOfWork.AccountTokens.GetAccountTokensByAccountAsync(accountId, cancellationToken);
            _unitOfWork.AccountTokens.RemoveRange(listAccountTokens);

            // Delete user firebase token associated with current access token
            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0) // Lock Succeeded 
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Lock,
                    Message = LocalizationString.Account.LockedAccount,
                    MessageParams = new object[] {accountId.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Lock,
                Message = LocalizationString.Account.FailedToLockAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount, Constants.JsonSerializerOptions)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Lock,
                Message = LocalizationString.Account.FailedToLockAccount,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    public async Task<Result<AccountResult>> UnlockAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Check account id
            var existedAccount = await _unitOfWork.Accounts.FindByIdAsync(accountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            // Check that account only can be unlocked if its current status is locked
            if (existedAccount.Status != AccountStatus.Locked)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotLockedYet].Value.ToErrors(_localizationService));

            // Check current account permission
            // If account is sysadmin --> can update
            // If account is tenant admin-> can update user who belong his tenant only --> return forbid http status code
            var hasTenantAdmin = _currentAccountService.HasPerm(Constants.Permissions.TenantAdmin);

            // Tenant admin
            if (hasTenantAdmin)
                if (existedAccount.CreatedById != _currentAccountService.Id)
                    return Result<AccountResult>.Forbid(_localizationService[LocalizationString.Account.PermissionDenied].Value.ToErrors(_localizationService));

            existedAccount.Status = AccountStatus.Active;

            // Add event enable account

            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0) // Unlock Succeeded 
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Unlock,
                    Message = LocalizationString.Account.Unlocked,
                    MessageParams = new object[] {accountId.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Unlock,
                Message = LocalizationString.Account.FailedToUnlock,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Unlock,
                Message = LocalizationString.Account.FailedToUnlock,
                MessageParams = new object[] {accountId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, nameof(UnlockAccountAsync));
            throw;
        }
    }

    #endregion

    #region Authorized User

    public async Task<Result<AccountResult>> ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;

            var currentAccount = await _unitOfWork.Accounts.FindByIdAsync(currentAccountId, cancellationToken);

            // Check current password
            var isCurrentPassword = _passwordGeneratorService.VerifyHashPassword(currentAccount?.PasswordHash, request.CurrentPassword);
            if (!isCurrentPassword)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.PasswordIncorrect].Value.ToErrors(_localizationService));

            // Change password
            currentAccount.PasswordHash = _passwordGeneratorService.HashPassword(request.NewPassword);

            // Check ForceOtherSessionsLogout, force all other sessions must be logged out or not
            if (request.ForceOtherSessionsLogout)
            {
                // remove all jwt token to force user logout
                var listAccountTokens = await _unitOfWork.AccountTokens.GetAccountTokensByAccountAsync(currentAccountId, cancellationToken);
                _unitOfWork.AccountTokens.RemoveRange(listAccountTokens);

                // Delete user firebase token associated with current access token
            }

            if (!request.ForceOtherSessionsLogout)
            {
                // Get current token
                var token = _currentAccountService.GetJwtToken();
                var accountToken = await _unitOfWork.AccountTokens.GetAccountTokensByTokenAsync(token, cancellationToken);
                // Get token firebase by token
                _unitOfWork.AccountTokens.Remove(accountToken);
            }

            _unitOfWork.Accounts.Update(currentAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.ChangeMyPassword,
                    Message = LocalizationString.Account.ChangedPasswordSuccess,
                    ExtraInfo = _jsonSerializerService.Serialize(currentAccount)
                }, cancellationToken);
                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Message = LocalizationString.Account.FailedToChangePassword,
                Action = Constants.Actions.Identity.Account.ChangeMyPassword,
                ExtraInfo = _jsonSerializerService.Serialize(request)
            }, cancellationToken);

            return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.FailedToChangePassword].Value.ToErrors(_localizationService));
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Message = LocalizationString.Account.FailedToChangePassword,
                Action = Constants.Actions.Identity.Account.ChangeMyPassword,
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, nameof(ChangePasswordAsync));
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AccountResult>> UpdateMyAccountAsync(Domain.Entities.Identity.Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            //Get current account
            var currentAccountId = _currentAccountService.Id;

            // Check account 
            var existedAccount = await _unitOfWork.Accounts.ViewMyAccountAsync(currentAccountId, cancellationToken);
            if (existedAccount == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));

            //Check duplicated email only
            var emailCheck = await _unitOfWork.Accounts.IsDuplicatedEmailAsync(existedAccount.Id, account.Email, cancellationToken);
            if (emailCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedEmail].Value.ToErrors(_localizationService));

            //Check duplicated phone 
            var phoneCheck = await _unitOfWork.Accounts.IsDuplicatedPhoneNumberAsync(existedAccount.Id, account.PhoneNumber, cancellationToken);
            if (phoneCheck)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.DuplicatedPhoneNumber].Value.ToErrors(_localizationService));

            //Using automapper
            _mapper.Map(account, existedAccount);

            _unitOfWork.Accounts.Update(existedAccount);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.UpdateMyAccount,
                    Message = LocalizationString.Account.UpdatedMyAccount,
                    MessageParams = new object[] {existedAccount.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
                }, cancellationToken);
                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.UpdateMyAccount,
                Message = LocalizationString.Account.FailedToUpdateMyAccount,
                ExtraInfo = _jsonSerializerService.Serialize(existedAccount)
            }, cancellationToken);
            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.UpdateMyAccount,
                Message = LocalizationString.Account.FailedToUpdateMyAccount,
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }

    #endregion

    #region Guest

    public async Task<Result<SignInWithUserNameResponse>> SignInWithUserNameAsync(SignInWithUserNameRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find the user with status is not inactive
            var account = await _unitOfWork.Accounts.GetAccountByUserNameAsync(request.UserName, cancellationToken);
            if (account == null)
                return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
            // If user is not active, return error
            switch (account.Status)
            {
                case AccountStatus.Inactive:
                    return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsNotActive].Value.ToErrors(_localizationService));
                case AccountStatus.PendingApproval:
                    return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingApproval].Value.ToErrors(_localizationService));
                // case AccountStatus.Locked:
                //     return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsLocked].Value.ToErrors(_localizationService));
                case AccountStatus.PendingConfirmation:
                    return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountIsPendingConfirmation].Value.ToErrors(_localizationService));
            }

            // Verify password or temporary password, if both of them are wrong, return
            if (!_passwordGeneratorService.VerifyHashPassword(account.PasswordHash, request.Password) &&
                !_passwordGeneratorService.VerifyHashPassword(account.PasswordHashTemporary, request.Password))
            {
                // If user lockout enabled, check them and process
                if (account.LockoutEnabled)
                {
                    account.AccessFailedCount += 1;
                    if (account.AccessFailedCount > _appOption.LockoutLimit - 1)
                    {
                        account.LockoutEnd = _dateTime.UtcNow.AddMinutes(_appOption.LockoutDurationInMin);
                        account.Status = AccountStatus.Locked;
                        _unitOfWork.Accounts.Update(account);
                        await _unitOfWork.CompleteAsync(account, cancellationToken);
                        return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.AccountLockedOut].Value.ToErrors(_localizationService));
                    }

                    _unitOfWork.Accounts.Update(account);
                    await _unitOfWork.CompleteAsync(account, cancellationToken);
                    return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithLockoutEnabled, _appOption.LockoutLimit - account.AccessFailedCount].Value.ToErrors(_localizationService));
                }

                return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.UserNameOrPasswordIncorrectWithoutLockOut].Value.ToErrors(_localizationService));
            }

            // Check lockout time, return error if we're still in the locked period
            if (account.LockoutEnd != null && account.LockoutEnd > _dateTime.UtcNow)
                return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.LockedOutAffected, account.LockoutEnd.ToFormattedString()].Value.ToErrors(_localizationService));
            // Check password expiration 
            if (account.PasswordValidUntilDate != null && account.PasswordValidUntilDate < _dateTime.UtcNow)
                return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.PasswordExpired].Value.ToErrors(_localizationService));
            // If we need to change password at first log in, force them to change
            if (account.PasswordChangeRequired)
                return Result<SignInWithUserNameResponse>.Fail(_localizationService[LocalizationString.Account.ChangePasswordRequired].Value.ToErrors(_localizationService));

            // Get roles + permissions

            var claimsIdentity = BuildClaimsIdentity(account);
            var tokenResponse = await _jwtService.GenerateJwtAsync(account, claimsIdentity, cancellationToken);
            account.AccountTokens = new List<AccountToken>()
            {
                new AccountToken()
                {
                    AccountId = account.Id,
                    Name = Guid.NewGuid().ToString().ToUpper(),
                    LoginProvider = Constants.LoginProviders.Self,
                    Token = tokenResponse.Data.Token,
                    RefreshToken = tokenResponse.Data.RefreshToken
                }
            };
            // Reset failed count and locked out date
            account.AccessFailedCount = 0;
            account.LockoutEnd = null;
            _unitOfWork.Accounts.Update(account);

            var result = await _unitOfWork.CompleteAsync(account, cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Message = LocalizationString.Account.LoggedIn,
                    Action = Constants.Actions.Identity.Account.SignInWithUserName,
                    MessageParams = new object[] {request.UserName},
                    ExtraInfo = _jsonSerializerService.Serialize(request)
                }, cancellationToken);
                return Result<SignInWithUserNameResponse>.Succeed(new SignInWithUserNameResponse()
                {
                    Token = tokenResponse.Data.Token,
                    RefreshToken = tokenResponse.Data.RefreshToken
                });
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Message = LocalizationString.Account.FailedToSignInWithUserName,
                Action = Constants.Actions.Identity.Account.SignInWithUserName,
                MessageParams = new object[] {request.UserName},
                ExtraInfo = _jsonSerializerService.Serialize(request)
            }, cancellationToken);
            return Result<SignInWithUserNameResponse>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Message = LocalizationString.Account.FailedToSignInWithUserName,
                Action = Constants.Actions.Identity.Account.SignInWithUserName,
                MessageParams = new object[] {request.UserName},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, nameof(SignInWithUserNameAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<AccountResult>> LogOutAsync(bool forceEndOtherSessions = false, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var currentAccountId = _currentAccountService.Id;
            var account = await _unitOfWork.Accounts.FindByIdAsync(currentAccountId, cancellationToken);
            if (account == null)
                return Result<AccountResult>.Fail(_localizationService[LocalizationString.Account.NotFound].Value.ToErrors(_localizationService));
            if (forceEndOtherSessions)
            {
                // remove all jwt token to force user logout
                var listAccountTokens = await _unitOfWork.AccountTokens.GetAccountTokensByAccountAsync(currentAccountId, cancellationToken);
                _unitOfWork.AccountTokens.RemoveRange(listAccountTokens);
            }

            if (!forceEndOtherSessions)
            {
                // Get current token
                var token = _currentAccountService.GetJwtToken();
                var accountToken = await _unitOfWork.AccountTokens.GetAccountTokensByTokenAsync(token, cancellationToken);
                // Get token firebase by token
                _unitOfWork.AccountTokens.Remove(accountToken);
            }

            _unitOfWork.Accounts.Update(account);

            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.Identity.Account.Logout,
                    Message = LocalizationString.Account.LoggedOut,
                    MessageParams = new object[] {currentAccountId.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(account)
                }, cancellationToken);

                return Result<AccountResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Logout,
                Message = LocalizationString.Account.FailedToLogOut,
                MessageParams = new object[] {currentAccountId.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(account)
            }, cancellationToken);

            return Result<AccountResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.Identity.Account.Logout,
                Message = LocalizationString.Account.FailedToLogOut,
                ExtraInfo = e.ToString()
            }, cancellationToken);
            _loggerService.LogCritical(e, e.ToString());
            throw;
        }
    }
    private static ClaimsIdentity BuildClaimsIdentity(Domain.Entities.Identity.Account account)
    {
        var claims = new List<Claim>();
        foreach (var userRole in account.AccountRoles)
        {
            if (userRole.Role == null) continue;
            claims.Add(new Claim(JwtClaimTypes.Role, userRole.Role.Name));

            claims.AddRange(from rolePermission in userRole.Role?.RolePermissions ?? new List<RolePermission>() where rolePermission.Permission != null select new Claim(JwtClaimTypes.Permission, rolePermission.Permission?.Code ?? string.Empty));
        }

        claims.Add(new Claim(JwtClaimTypes.IdentityProvider, Constants.LoginProviders.Self));
        claims.Add(new Claim(JwtClaimTypes.UserId, account.Id.ToString()));

        var claimsIdentity = new ClaimsIdentity(claims);
        return claimsIdentity;
    }

    #endregion
}
