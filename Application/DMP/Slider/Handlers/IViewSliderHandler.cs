using Application.Common.Models;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Slider.Responses;
using MediatR;

namespace Application.DMP.Slider.Handlers;
public interface IViewSliderHandler: IRequestHandler<ViewSliderQuery, Result<ViewSliderResponse>>
{
    
}
