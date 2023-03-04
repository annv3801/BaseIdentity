using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;

namespace Application.Identity.Account.Repositories;
public interface IAccountRepository : IRepository<Domain.Entities.Identity.Account>
{
    Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedEmailAsync(Guid accountId,string email, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsDuplicatedPhoneNumberAsync(Guid accountId, string? phone, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<Domain.Entities.Identity.Account>> GetAccountsByRoleAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account> SignInByPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account> SignInByOathAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<bool> IsValidJwtAsync(Guid accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.Identity.Account>> ViewListAccountsByAdminAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Account> RegisterAccountAsync(string role, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.Identity.Account>> ViewListNhaCungCapAsync(CancellationToken cancellationToken = default(CancellationToken));
}
