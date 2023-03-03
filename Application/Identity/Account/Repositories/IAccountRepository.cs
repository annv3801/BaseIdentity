using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Application.Identity.Account.Repositories
{
    public interface IAccountRepository : IRepository<Domain.Entities.Identity.Account>
    {
        /// <summary>
        /// Get user account by phone
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));
        Task<Domain.Entities.Identity.Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Check duplicate email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="accountId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsDuplicatedEmailAsync(Guid accountId,string email, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// Check duplicate phone number
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsDuplicatedPhoneNumberAsync(Guid accountId, string? phone, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get user account by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<Domain.Entities.Identity.Account>> GetAccountsByRoleAsync(string role, CancellationToken cancellationToken = default(CancellationToken));

        Task<Domain.Entities.Identity.Account> SignInByPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// View account detail by admin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(Guid userId, CancellationToken cancellationToken = default(CancellationToken));

        Task<Domain.Entities.Identity.Account> SignInByOathAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// The user view his account detail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(Guid userId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Check validate Jwt
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="jwt"></param>
        /// <param name="loginProvider"></param>
        /// <param name="cancellationToken"></param>
        Task<bool> IsValidJwtAsync(Guid accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// View list account by admin
        /// </summary>
        /// <param name="cancellationToken"></param>
        Task<IQueryable<Domain.Entities.Identity.Account>> ViewListAccountsByAdminAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<User> ForgotPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        //Task<User> ChangeMyPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        // Task<User> SetNewPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        Task<Domain.Entities.Identity.Account> RegisterAccountAsync(string role, CancellationToken cancellationToken = default(CancellationToken));

        //Task<User> DisableUserAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        // Task<User> EnableUserAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
        Task<IQueryable<Domain.Entities.Identity.Account>> ViewListNhaCungCapAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}