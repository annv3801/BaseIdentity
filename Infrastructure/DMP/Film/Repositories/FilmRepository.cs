using System.Linq.Dynamic.Core;
using Application.Common.Interfaces;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Film.Repositories;
public class FilmRepository : Repository<Domain.Entities.DMP.Film>, IFilmRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public FilmRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Domain.Entities.DMP.Film?> GetFilmAsync(Guid filmId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Films.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == filmId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Film>> ViewListFilmsAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Films
            .AsSplitQuery()
            .AsQueryable();
    }

    public async Task<Domain.Entities.DMP.Film?> GetFilmByShortenUrlAsync(string url, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Films.AsSplitQuery().FirstOrDefaultAsync(r => r.ShortenUrl == url, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Film>> ViewListFilmByCategoryAsync(ViewListFilmByCategoryQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        var category =  await _applicationDbContext.Categories.Where(x => x.ShortenUrl == query.CategorySlug).FirstOrDefaultAsync(cancellationToken);
        return _applicationDbContext.Films.Where(x => x.CategoryId == category.Id)
            .AsSplitQuery()
            .AsQueryable();
    }
}
