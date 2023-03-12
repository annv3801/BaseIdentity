using Application.Common.Interfaces;

namespace Application.DMP.Theater.Repositories;
public interface ITheaterRepository : IRepository<Domain.Entities.DMP.Theater>
{
    Task<Domain.Entities.DMP.Theater?> GetTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Theater>> ViewListTheatersAsync(CancellationToken cancellationToken = default(CancellationToken));

}
