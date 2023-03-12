using Application.Common.Interfaces;

namespace Application.DMP.Film.Repositories;
public interface IFilmRepository : IRepository<Domain.Entities.DMP.Films>
{
    Task<Domain.Entities.DMP.Films?> GetFilmAsync(Guid filmId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Films>> ViewListFilmsAsync(CancellationToken cancellationToken = default(CancellationToken));
}
