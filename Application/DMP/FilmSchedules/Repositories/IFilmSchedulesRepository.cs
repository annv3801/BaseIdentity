using Application.Common.Interfaces;
using Application.DMP.FilmSchedules.Queries;
using Domain.Entities.DMP;

namespace Application.DMP.FilmSchedules.Repositories;
public interface IFilmSchedulesRepository : IRepository<Domain.Entities.DMP.FilmSchedule>
{
    Task<FilmSchedule?> GetFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<FilmSchedule>> ViewListFilmSchedulesAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<FilmSchedule>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery request, CancellationToken cancellationToken = default(CancellationToken));
}
