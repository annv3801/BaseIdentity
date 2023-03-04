using Application.Common.Interfaces;
using Application.Identity.Permission.Queries;

namespace Application.Identity.Permission.Repositories;
public interface IPermissionRepository : IRepository<Domain.Entities.Identity.Permission>
{
    Task<Domain.Entities.Identity.Permission?> FindPermissionById(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Permission?> FindPermissionByIdWithAuditableEntity(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.Identity.Permission>> SearchPermissionByName(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken));

  
}
