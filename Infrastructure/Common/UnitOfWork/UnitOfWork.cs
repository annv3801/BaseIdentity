using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.DMP.Category.Repositories;
using Application.Identity.Account.Repositories;
using Application.Identity.Permission.Repositories;
using Application.Identity.Role.Repositories;
using Domain.Entities.Identity;
using Infrastructure.DMP.Category.Repositories;
using Infrastructure.Identity.Account.Repositories;
using Infrastructure.Identity.Permission.Repositories;
using Infrastructure.Identity.Role.Repositories;

namespace Infrastructure.Common.UnitOfWork;
[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UnitOfWork(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        Roles = new RoleRepository(_applicationDbContext);
        Permissions = new PermissionRepository(_applicationDbContext);
        AccountTokens = new AccountTokenRepository(_applicationDbContext);
        Accounts = new AccountRepository(_applicationDbContext);
        AccountLogins = new AccountLoginRepository(_applicationDbContext);
        Categories = new CategoryRepository(_applicationDbContext);
    }

    public IAccountTokenRepository AccountTokens { get; }
    public IAccountRepository Accounts { get; }
    public IRoleRepository Roles { get; }
    public IPermissionRepository Permissions { get; }
    public IAccountLoginRepository AccountLogins { get; }
    public ICategoryRepository Categories { get; }
    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CompleteAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.SaveChangesAsync(account, cancellationToken);
    }
}
