using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Permission.Queries;
using Application.Identity.Permission.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Permission.Repositories
{
    
    public class PermissionRepository : Repository<Domain.Entities.Identity.Permission>, IPermissionRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public PermissionRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        ///<inheritdoc/>
        public async Task<Domain.Entities.Identity.Permission?> FindPermissionById(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Permissions.FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Permission?> FindPermissionByIdWithAuditableEntity(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Permissions
                .Include(p => p.CreatedBy)
                .Include(p => p.LastModifiedBy)
                .FirstOrDefaultAsync(p => p.Id == permissionId, cancellationToken);
        }

        public async Task<IQueryable<Domain.Entities.Identity.Permission>> SearchPermissionByName(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var keyword = query.Keyword?.ToUpper() ?? string.Empty;
            await Task.CompletedTask;
            return _applicationDbContext.Permissions
                .Include(p => p.CreatedBy)
                .Include(p => p.LastModifiedBy)
                .Where(p => keyword.Length == 0 || p.NormalizedName.Contains(keyword))
                .AsSplitQuery();
        }

      
    }
}