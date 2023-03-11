using Application.Common.Interfaces;

namespace Application.DMP.Category.Repositories;
public interface ICategoryRepository : IRepository<Domain.Entities.DMP.Category>
{
    Task<Domain.Entities.DMP.Category?> GetCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Category>> ViewListCategoriesAsync(CancellationToken cancellationToken = default(CancellationToken));

}
