using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Interface for application db context.
    /// Extending IDisposable interface to allow us to dispose application db context in unit of work
    /// </summary>
    public interface IApplicationDbContext
    {
        #region DbSet

        DbSet<Account> Accounts { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<AccountToken> AccountTokens { get; set; }
        DbSet<AccountLogin> AccountLogins { get; set; }
        DbSet<Permission> Permissions { get; set; }

        #endregion


        public DbContext DbContext { get; }
        public DatabaseFacade Database { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(Account account, CancellationToken cancellationToken = default(CancellationToken));
    }
}