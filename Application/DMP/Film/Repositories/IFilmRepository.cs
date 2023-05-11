using Application.Common.Interfaces;
using Application.DMP.Film.Queries;

namespace Application.DMP.Film.Repositories;
public interface IFilmRepository : IRepository<Domain.Entities.DMP.Film>
{
    Task<Domain.Entities.DMP.Film?> GetFilmAsync(Guid filmId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Film>> ViewListFilmsAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.DMP.Film?> GetFilmByShortenUrlAsync(string url, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Film>> ViewListFilmByCategoryAsync(ViewListFilmByCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
