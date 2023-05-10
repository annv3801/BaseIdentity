using Application.Common.Models;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Commons;
using MediatR;

namespace Application.DMP.Slider.Handlers;

public interface ICreateSliderHandlers : IRequestHandler<CreateSliderCommand, Result<SliderResult>>
{
    
}