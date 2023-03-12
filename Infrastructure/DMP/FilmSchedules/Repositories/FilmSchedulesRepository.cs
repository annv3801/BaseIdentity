using Application.Common.Interfaces;
using Application.DMP.FilmSchedules.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.FilmSchedules.Repositories;
public class FilmSchedulesRepository : Repository<Domain.Entities.DMP.FilmSchedules>, IFilmSchedulesRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public FilmSchedulesRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.FilmSchedules?> GetFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.FilmSchedules.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == filmScheduleId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.FilmSchedules>> ViewListFilmSchedulesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.FilmSchedules
            .AsSplitQuery()
            .AsQueryable();
    }

}
