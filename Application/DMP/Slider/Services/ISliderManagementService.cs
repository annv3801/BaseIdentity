using Application.Common.Models;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Queries;
using Application.DMP.Slider.Commons;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Slider.Services;
public interface ISliderManagementService
{
    Task<Result<SliderResult>> CreateSliderAsync(Domain.Entities.DMP.Slider slider, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewSliderResponse>> ViewSliderAsync(Guid sliderId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewSliderResponse>>> ViewListSliderAsync(ViewListSlidersQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
