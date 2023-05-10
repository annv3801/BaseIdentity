using Application.Common.Interfaces;

namespace Application.DMP.Slider.Repositories;
public interface ISliderRepository : IRepository<Domain.Entities.DMP.Slider>
{
    Task<Domain.Entities.DMP.Slider?> GetSliderAsync(Guid sliderId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Slider>> ViewListSlidersAsync(CancellationToken cancellationToken = default(CancellationToken));
}
