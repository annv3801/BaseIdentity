using Application.Common.Interfaces;
using Application.Identity.Account.Repositories;
using Domain.Entities.Identity;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Account.Repositories;
public class AccountLoginRepository: Repository<AccountLogin>, IAccountLoginRepository
{
    private readonly IApplicationDbContext _applicationDbContext;
    public AccountLoginRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.Identity.Account?> GetAccountByAccountLogin(string email, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Accounts
            .Include(u => u.AccountRoles)
            .ThenInclude(r => r.Role)
            .ThenInclude(rp => rp.RolePermissions)
            .ThenInclude(p => p.Permission)
            .Include(u => u.AccountLogins)
            .AsSplitQuery()
            .Where(u => u.AccountLogins != null && u.AccountLogins.Any(ur =>ur.LoginProvider == loginProvider && ur.ProviderKey==providerKey && u.Email==email))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
