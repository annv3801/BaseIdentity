using Application.Common.Interfaces;
using Application.Identity.Role.Queries;

namespace Application.Identity.Role.Repositories;
public interface IRoleRepository : IRepository<Domain.Entities.Identity.Role>
{
    Task<Domain.Entities.Identity.Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Role?> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<List<Domain.Entities.Identity.Role>?> GetRolesAsync(List<Guid> roleIds, CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.Identity.Role?> GetRoleToViewDetail(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.Identity.Role>> SearchRoleByName(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));
}
