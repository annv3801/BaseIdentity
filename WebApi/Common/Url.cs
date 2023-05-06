using System.Diagnostics.CodeAnalysis;

namespace WebApi.Common;
[ExcludeFromCodeCoverage]
public static class Url
{
    public static class Areas
    {
        public const string Identity = "Identity";
        public const string Resource = "Resource";
        public const string Tenant = "Tenant";
        public const string DMP = "DMP";
    }

    public static class Identity
    {
        public static class Account
        {
            public const string Create = "Identity/Account";
            public const string SignInWithPassword = "Identity/Account/SignIn";
            public const string SignInWithOAuth = "Identity/Account/SignInWithOAuth";
            public const string SignInWithPo = "Identity/Account/SignInWithPO";
            public const string ActivateAccount = "Identity/Account/Activation/{accountId}";
            public const string DeactivateAccount = "Identity/Account/Activation/{accountId}";
            public const string Activate = "Identity/Account/Activation";
            public const string Delete = "Identity/Account/{accountId}";
            public const string View = "Identity/Account/{accountId}";
            public const string ViewList = "Identity/Account/List";
            public const string ViewMytAccount = "Identity/Account/MyProfile";
            public const string Lock = "Identity/Account/Lock/{accountId}";
            public const string Unlock = "Identity/Account/Lock/{accountId}";
            public const string UpdateMytAccount = "Identity/Account";
            public const string UpdateAccount = "Identity/Account/{accountId}";
            public const string Logout = "Identity/Account/Logout";
            public const string ForgotMyPassword = "Identity/Account/ForgotPassword";
            public const string ChangePassword = "Identity/Account/ChangePassword";
            public const string SetAccountPassword = "Identity/Account/SetPassword";
            public const string UpdateAccountFcmToken = "Identity/Account/FirebaseToken";
            public const string SetMyNewPassword = "Identity/Account/SetMyNewPassword";
            public const string ChangePasswordAtFirstLogin = "Identity/Account/ChangePasswordFirstLogin";
            public const string SignInWithUserName = "Identity/Account/SignInWithUserName";
        }

        public static class Permission
        {
            public const string Update = "Identity/Permission/{permId}";
            public const string View = "Identity/Permission/{permId}";
            public const string ViewList = "Identity/Permission";
        }

        public static class Role
        {
            public const string Create = "Identity/Role";
            public const string Update = "Identity/Role/{roleId}";
            public const string View = "Identity/Role/{roleId}";
            public const string Delete = "Identity/Role/{roleId}";
            public const string Activate = "Identity/Role/Status/{roleId}";
            public const string Deactivate = "Identity/Role/Status/{roleId}";
            public const string ViewList = "Identity/Role";
        }
    }

    public static class DMP
    {
        public static class Booking
        {
            public const string Create = "DMP/Booking";
            
        }
        public static class Category
        {
            public const string Create = "DMP/Category";
            public const string View = "DMP/Category/{categoryId}";
            public const string Delete = "DMP/Category/{categoryId}";
            public const string Update = "DMP/Category/{categoryId}";
            public const string ViewList = "DMP/Category";
        }

        public static class Email
        {
            public const string SendEmail = "DMP/Email";
        }
        public static class Film
        {
            public const string Create = "DMP/Film";
            public const string View = "DMP/Film/{filmId}";
            public const string Delete = "DMP/Film/{filmId}";
            public const string Update = "DMP/Film/{filmId}";
            public const string ViewList = "DMP/Film";
            public const string ViewByShortenUrl = "DMP/Film-By-ShortenUrl/{shortenUrl}";

        }
        public static class Theater
        {
            public const string Create = "DMP/Theater";
            public const string View = "DMP/Theater/{theaterId}";
            public const string Delete = "DMP/Theater/{theaterId}";
            public const string Update = "DMP/Theater/{theaterId}";
            public const string ViewList = "DMP/Theater";
        }
        public static class Room
        {
            public const string Create = "DMP/Room";
            public const string View = "DMP/Room/{roomId}";
            public const string Delete = "DMP/Room/{roomId}";
            public const string Update = "DMP/Room/{roomId}";
            public const string ViewList = "DMP/Room";
        }
        public static class FilmSchedules
        {
            public const string Create = "DMP/FilmSchedules";
            public const string View = "DMP/FilmSchedules/{filmScheduleId}";
            public const string Delete = "DMP/FilmSchedules/{filmScheduleId}";
            public const string Update = "DMP/FilmSchedules/{filmScheduleId}";
            public const string ViewList = "DMP/FilmSchedules";
            public const string ViewByFilmId = "DMP/FilmSchedulesByFilmId";

        }
        public static class Seat
        {
            public const string Create = "DMP/Seat";
            public const string View = "DMP/Seat/{seatId}";
            public const string Delete = "DMP/Seat/{seatId}";
            public const string Update = "DMP/Seat/{seatId}";
            public const string ViewList = "DMP/Seat";
            public const string ViewListBySchedule = "DMP/SeatBySchedule";
        }
        public static class Ticket
        {
            public const string Create = "DMP/Ticket";
            public const string View = "DMP/Ticket/{ticketId}";
            public const string Delete = "DMP/Ticket/{ticketId}";
            public const string Update = "DMP/Ticket/{ticketId}";
            public const string ViewList = "DMP/Ticket";
        }
    }
}
