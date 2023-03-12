using Application.Common.Interfaces;
using Application.DMP.Theater.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Theater.Repositories;
public class TheaterRepository : Repository<Domain.Entities.DMP.Theater>, ITheaterRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public TheaterRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.Theater?> GetTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Theaters.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == theaterId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Theater>> ViewListTheatersAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Theaters
            .AsSplitQuery()
            .AsQueryable();
    }

}
