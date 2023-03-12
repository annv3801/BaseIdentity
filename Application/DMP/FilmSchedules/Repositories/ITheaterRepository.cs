using Application.Common.Interfaces;

namespace Application.DMP.FilmSchedules.Repositories;
public interface IFilmSchedulesRepository : IRepository<Domain.Entities.DMP.FilmSchedules>
{
    Task<Domain.Entities.DMP.FilmSchedules?> GetFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.FilmSchedules>> ViewListFilmSchedulesAsync(CancellationToken cancellationToken = default(CancellationToken));

}
