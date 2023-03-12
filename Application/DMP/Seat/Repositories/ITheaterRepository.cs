using Application.Common.Interfaces;

namespace Application.DMP.Seat.Repositories;
public interface ISeatRepository : IRepository<Domain.Entities.DMP.Seat>
{
    Task<Domain.Entities.DMP.Seat?> GetSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Seat>> ViewListSeatsAsync(CancellationToken cancellationToken = default(CancellationToken));

}
