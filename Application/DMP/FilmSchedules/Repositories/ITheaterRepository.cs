using Application.Common.Interfaces;

namespace Application.DMP.FilmSchedules.Repositories;
public interface IFilmSchedulesRepository : IRepository<Domain.Entities.DMP.FilmSchedule>
{
    Task<Domain.Entities.DMP.FilmSchedule?> GetFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.FilmSchedule>> ViewListFilmSchedulesAsync(CancellationToken cancellationToken = default(CancellationToken));

}
