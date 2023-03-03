using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Permission.Queries;

namespace Application.Identity.Permission.Repositories
{
    public interface IPermissionRepository : IRepository<Domain.Entities.Identity.Permission>
    {
        /// <summary>
        /// Find active permission by its id
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Permission?> FindPermissionById(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Find permission by id and include the created by and last modified by
        /// </summary>
        /// <param name="permissionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Domain.Entities.Identity.Permission?> FindPermissionByIdWithAuditableEntity(Guid permissionId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Search for Permissions by the name
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<Domain.Entities.Identity.Permission>> SearchPermissionByName(ViewListPermissionsQuery query, CancellationToken cancellationToken = default(CancellationToken));

      
    }
}