using Application.Common.Interfaces;
using Application.Identity.Role.Queries;
using Application.Identity.Role.Repositories;
using Domain.Enums;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Role.Repositories;
public class RoleRepository : Repository<Domain.Entities.Identity.Role>, IRoleRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public RoleRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.Identity.Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Roles.Include(r => r.RolePermissions).Where(r => r.NormalizedName == name.ToUpper()).AsSplitQuery().FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Role?> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Roles.Include(r => r.RolePermissions).AsSplitQuery().FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
    }

    public async Task<List<Domain.Entities.Identity.Role>?> GetRolesAsync(List<Guid> roleIds, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Roles.Where(r => roleIds.Contains(r.Id) && r.Status == RoleStatus.Active)
            .AsSplitQuery().ToListAsync(cancellationToken);
    }

    public async Task<Domain.Entities.Identity.Role?> GetRoleToViewDetail(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Roles
            .Include(r => r.CreatedBy)
            .Include(r => r.LastModifiedBy)
            .Include(r => r.RolePermissions)
            .ThenInclude(p => p.Permission)
            .AsSplitQuery()
            .FirstOrDefaultAsync(r => r.Id == roleId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.Identity.Role>> SearchRoleByName(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        var keyword = query.Keyword?.ToUpper() ?? string.Empty;
        return _applicationDbContext.Roles
            .Include(r => r.CreatedBy)
            .Include(r => r.LastModifiedBy)
            .Include(r => r.RolePermissions)
            .ThenInclude(p => p.Permission)
            .Where(r => keyword.Length == 0 || r.NormalizedName.Contains(keyword))
            .AsSplitQuery();
    }
}
