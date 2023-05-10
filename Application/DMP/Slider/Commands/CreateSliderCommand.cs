using Application.Common.Models;
using Application.DMP.Slider.Commons;
using Application.DTO.DMP.Film.Requests;
using Application.DTO.DMP.Slider.Requests;
using MediatR;

namespace Application.DMP.Slider.Commands;

public class CreateSliderCommand : CreateSliderRequest, IRequest<Result<SliderResult>>
{
    
}