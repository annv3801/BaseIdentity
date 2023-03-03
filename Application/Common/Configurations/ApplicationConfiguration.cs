using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Configurations
{
    /// <summary>
    /// Configure other application stuff
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ApplicationConfiguration
    {
        /// <summary>
        /// Maximum allowed account activation failed count (compare with field AccessFailedCount of account)
        /// </summary>
        public int MaximumAccountActivationFailedCount { get; set; } = 10;
        /// <summary>
        /// Number of maximum of sending otp per task. E.g. Max no. of sending OTP for forgot password request, register account, ..
        /// </summary>
        public int MaximumOtpPerDay { get; set; } = 10;
        /// <summary>
        /// If true, the user registration will require OTP confirmation process
        /// </summary>
        public bool EnableRegistrationWithOtp { get; set; } = true;

        /// <summary>
        /// If true, request will check on captcha verification
        /// </summary>
        public bool EnableRecaptchaWithGuest { get; set; } = true;

        /// <summary>
        /// Time amount to lock out an account
        /// </summary>
        public int LockoutDurationInMin { get; set; } = 30;

        /// <summary>
        /// The number of lock out counters before locking out
        /// </summary>
        public int LockoutLimit { get; set; } = 5;
    }
}