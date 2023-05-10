using Application.Common.Models;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Slider.Handlers;
public interface IViewListSlidersHandler: IRequestHandler<ViewListSlidersQuery, Result<PaginationBaseResponse<ViewSliderResponse>>>
{
    
}
