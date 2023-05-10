using Application.DMP.Film.Commands;
using Application.DMP.Film.Queries;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Film.Requests;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Requests;
using Application.DTO.DMP.Slider.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Slider.Mappings;
public class SliderProfile : Profile
{
    public SliderProfile()
    {
        CreateMap<CreateSliderRequest, CreateSliderCommand>();

        CreateMap<CreateSliderCommand, Domain.Entities.DMP.Slider>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
            .ForMember(d => d.ImageFile, o => o.MapFrom(s => s.Image));
        CreateMap<Domain.Entities.DMP.Slider, ViewSliderResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image));
        CreateMap<ViewListSlidersRequest, ViewListSlidersQuery>();
        CreateMap<ViewListSlidersRequest, ViewSliderQuery>();
    }
}
