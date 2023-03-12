using Domain.Entities.DMP;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    #region DMP

    DbSet<Category> Categories { get; set; }
    DbSet<Films> Films { get; set; }
    DbSet<Theater> Theaters { get; set; }
    DbSet<Room> Rooms { get; set; }
    DbSet<FilmSchedules> FilmSchedules { get; set; }

    #endregion
    
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
