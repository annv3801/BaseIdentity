using Application.Common.Interfaces;
using Application.DMP.Seat.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Seat.Repositories;
public class SeatRepository : Repository<Domain.Entities.DMP.Seat>, ISeatRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public SeatRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.Seat?> GetSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Seats.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == seatId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Seat>> ViewListSeatsAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Seats
            .AsSplitQuery()
            .AsQueryable();
    }

}
