using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Role.Queries;

namespace Application.Identity.Role.Repositories
{
    public interface IRoleRepository : IRepository<Domain.Entities.Identity.Role>
    {
        /// <summary>
        /// Get role by its name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Role?> GetRoleAsync(string name, CancellationToken cancellationToken = default(CancellationToken));

        Task<Domain.Entities.Identity.Role?> GetRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get roles by its id
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<Domain.Entities.Identity.Role>?> GetRolesAsync(List<Guid> roleIds, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get a role with created by, last modified by and list perms
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Role?> GetRoleToViewDetail(Guid roleId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Search for role by its name
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<Domain.Entities.Identity.Role>> SearchRoleByName(ViewListRolesQuery query, CancellationToken cancellationToken = default(CancellationToken));
    }
}