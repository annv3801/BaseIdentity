using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Responses;
using MediatR;

namespace Application.DMP.Slider.Queries;
[ExcludeFromCodeCoverage]
public class ViewSliderQuery : IRequest<Result<ViewSliderResponse>>
{
    public Guid Id { get; set; }
}
