using Application.Common.Interfaces;

namespace Application.DMP.News.Repositories;
public interface INewsRepository : IRepository<Domain.Entities.DMP.News>
{
    Task<Domain.Entities.DMP.News?> GetNewsAsync(Guid newsId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.News>> ViewListNewsAsync(CancellationToken cancellationToken = default(CancellationToken));
}
