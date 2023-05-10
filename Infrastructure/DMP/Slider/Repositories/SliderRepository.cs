using Application.Common.Interfaces;
using Application.DMP.Film.Repositories;
using Application.DMP.Slider.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Slider.Repositories;
public class SliderRepository : Repository<Domain.Entities.DMP.Slider>, ISliderRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public SliderRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Domain.Entities.DMP.Slider?> GetSliderAsync(Guid sliderId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Sliders.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == sliderId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Slider>> ViewListSlidersAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Sliders
            .AsSplitQuery()
            .AsQueryable();
    }
}
