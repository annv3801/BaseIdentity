using System.Linq.Dynamic.Core;
using Application.Common.Interfaces;
using Application.DMP.Category.Repositories;
using Domain.Enums;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Category.Repositories;
public class CategoryRepository : Repository<Domain.Entities.DMP.Category>, ICategoryRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CategoryRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.Category?> GetCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Categories.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == categoryId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Category>> ViewListCategoriesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Categories
            .AsSplitQuery()
            .AsQueryable();
    }

}
