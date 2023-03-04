using System.Diagnostics.CodeAnalysis;

namespace Application.Common;
[ExcludeFromCodeCoverage]
public static class LocalizationString
{
    public static class Common
    {
        public const string Success = "Success";
        public const string Error = "Error";
        public const string DataValidationError = "Data validation error";
        public const string UnknownFieldName = "General";
        public const string CommitFailed = "Cannot finish you action at this time, try again";
        public const string ViewListFailed = "Cannot finish you action at this time, try again";
        public const string InvalidRecaptcha = "Captcha is not valid";
        public const string ItemNotFound = "Item is not found, deactivated or deleted";
        public const string EmptyField = "{PropertyName} is empty";
        public const string NullField = "{PropertyName} is null";
        public const string IncorrectFormatField = "{PropertyName} ({PropertyValue}) format is not correct";
        public const string MaxLengthField = "The length of {PropertyName} must be {MaxLength} characters or fewer. You entered {TotalLength} characters";
        public const string MinLengthField = "The length of {PropertyName} must be at least {MaxLength} characters. You entered {TotalLength} characters";
        public const string DuplicatedItem = "Item is duplicated";
        public const string DuplicatedField = "{PropertyName} is duplicated";
        public const string DisabledFeature = "Feature is disabled";
        public const string NotValidEnumValue = "{PropertyName} value ({PropertyValue}) is not valid";
        public const string NotEmpty = "{PropertyName} value ({PropertyValue}) is required";
    }

    public static class Account
    {
        public const string FailedToCreateAccount = "Failed to create account {0}";
        public const string CreatedAccount = "Created Account {0}";
        public const string FailedToSignInWithPhoneNumber = "Failed to sign in with phone number {0}";
        public const string FailedToSignInWithUserName = "Failed to sign in with user name {0}";
        public const string NotFound = "Account is not found, deactivated or deleted";
        public const string AccountIsNotActive = "Your account is not active, you must activate your account first";
        public const string NotLockedYet= "Account is not locked yet, you can not unlock account {0}";
        public const string AccountIsPendingApproval = "Your account is pending for approval";
        public const string AccountIsLocked = "Your account is locked";
        public const string AccountIsPendingConfirmation = "Your account is pending for your confirmation";
        public const string AccountIsNotPendingConfirmation = "Your account is not pending for your confirmation, you can not activate Account";
        public const string PhoneNumberOrPasswordIncorrectWithLockoutEnabled = "Phone Number or password is incorrect ({0} times left)";
        public const string UserNameOrPasswordIncorrectWithLockoutEnabled = "User name or password is incorrect ({0} times left)";
        public const string PhoneNumberOrPasswordIncorrectWithoutLockOut = "Phone Number or password is incorrect";
        public const string UserNameOrPasswordIncorrectWithoutLockOut = "User name or password is incorrect";
        public const string ChangePasswordRequired = "You need to change your password first";
        public const string PasswordExpired = "Your password is expired";
        public const string LockedOutAffected = "You account is temporarily being locked out, it will be unlocked at {0}";
        public const string ExceedSendCode   = "You account just allow send maximun {0} OTP Sms per day";
        public const string AccountLockedOut = "Your account has been locked out";
        public const string NotLogin = "You are not logged in yet";
        public const string PasswordIncorrect = "Password is incorrect, please try again";
        public const string ChangedPasswordSuccess = "Password changed successfully ";
        public const string FailedToChangePassword = "Failed to change password";
        public const string FailedToLockAccount = "Failed to lock Account {0}";
        public const string LockedYet= "Account is locked yet, you can not lock account";
        public const string LockedAccount = "Locked Account {0}";
        public const string PermissionDenied = "Permission denied, You can update account who belong your tenant only ";
        public const string Unlocked = "Unlocked Account {0}";        
        public const string FailedToUnlock = "Failed to unlock Account {0}";
        public const string AccountIsNotInActive = "Your account is not inactive, you can not activate your account";
        public const string ActivatedAccount = "Activated Account {0} ";
        public const string FailedToActiveAccount = "Failed to active account {0}";
        public const string AccountIsDeactivate = "Your account is already in inactive status, you can not deactivate";
        public const string DeactivatedAccount = "Deactivated account {0}";
        public const string FailedToDeactivateAccount = "Failed to deactivate account {0}";
        public const string AccountIsDeleted = "Your account is deleted, you can not delete account again";
        public const string DeletedAccount = "Deleted Account {0}";
        public const string FailedToDeleteAccount = "Failed to delete account {0}";
        public const string FailedToViewAccountDetailByAdmin = "Failed to view account {0} by admin";
        public const string ViewedAccountDetailByAdmin = "Viewed account {0} by admin";
        public const string EmptyAccountList = "No account ";
        public const string ViewedListAccountByAdmin = "Viewed list account ";
        public const string FailedToViewListAccount = "Failed to view list account";
        public const string DuplicatedEmail = "Email is duplicated";
        public const string DuplicatedPhoneNumber = "PhoneNumber is duplicated";
        public const string UpdatedMyAccount = "Updated your account";
        public const string FailedToUpdateMyAccount = "Failed to update your account";
        public const string UpdatedAccount = "Updated Account {0}";
        public const string FailedToUpdateAccount = "Failed to update Account {0}";
        public const string LoggedOut = "You are logged out";
        public const string FailedToLogOut = "Failed to log out ";
        public const string FailedToActivateMyAccount = "Failed to activate your account";
        public const string WrongOtp = "Your Otp is incorrect. Please try again ";
        public const string ExpiredOtp = "Your Otp is expired. Please try again ";
        public const string FailedToForgotPassword = "Failed to forgot account password {0}";
        public const string ForgotPassword = "Forgot account password {0}";
        public const string SetPassword = "Account password {0} is set ";
        public const string FailedToSetPassword = "Failed to set account password {0}";
        public const string ChangePasswordNotRequired ="It seems you are not required to use this function to change password, please use change password function";
        public const string LoggedIn = "You are logged in";
    }

    public static class Permission
    {
        public const string Updated = "Updated Permission {0}";
        public const string FailedToUpdate = "Failed to update Permission {0}";
        public const string Duplicated = "Permission are duplicated";
        public const string NotFound = "Permission is not found or deleted";
    }
    public static class Role
    {
        public const string Duplicated = "Role is duplicated";
        public const string NotFound = "Role is not found, deactivated  or deleted";
        public const string PermissionsRequired = "Role must contain at least one permission";

        public const string Updated = "Updated Role {0}";
        public const string FailedToUpdate = "Failed to update Role {0}";

        public const string Created = "Created Role {0}";
        public const string FailedToCreate = "Failed to create Role {0}";

        public const string Deleted = "Deleted Role {0}";
        public const string FailedToDelete = "Failed to delete Role {0}";
        public const string AlreadyDeleted = "Role is already deleted, you cannot delete it again";

        public const string Activated = "Activated Role {0}";
        public const string FailedToActivate = "Failed to activate Role {0}";
        public const string AlreadyActivated = "Role is already activated, you cannot activated it again";

        public const string Deactivated = "Deactivated Role {0}";
        public const string FailedToDeactivate = "Failed to deactivate Role {0}";
        public const string AlreadyDeactivated = "Role is already deactivated, you cannot deactivate it again";
    }

    public static class PasswordValidation
    {
        public const string UniqueCharsField = "The number character of {PropertyName} must be at least {UniqueCharacterRequired} unique characters. You entered {TotalUniqueCharacter} unique characters";

        public const string NonAlphanumericField = "The {PropertyName} required special characters. Your password do not have special characters. Please try again";

        public const string LowerCaseField = "The {PropertyName} must have at least one lowercase ('a'-'z'). Please try again";
        public const string UpperCaseField = "The {PropertyName} must have at least one uppercase ('A'-'Z'). Please try again";
        public const string DigitField = "The {PropertyName} must have at least one digit ('0'-'9'). Please try again";
        public const string FailedToConfirmPassword = "Your confirm password is not same as your new password ";
    }
}
