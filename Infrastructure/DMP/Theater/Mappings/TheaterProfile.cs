using Application.DMP.Theater.Commands;
using Application.DMP.Theater.Queries;
using Application.DTO.DMP.Theater.Requests;
using Application.DTO.DMP.Theater.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Theater.Mappings;
public class TheaterProfile : Profile
{
    public TheaterProfile()
    {
        CreateMap<CreateTheaterRequest, CreateTheaterCommand>();

        CreateMap<CreateTheaterCommand, Domain.Entities.DMP.Theater>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<Domain.Entities.DMP.Theater, ViewTheaterResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<UpdateTheaterRequest, UpdateTheaterCommand>();
        CreateMap<UpdateTheaterCommand, Domain.Entities.DMP.Theater>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<ViewListTheatersRequest, ViewListTheatersQuery>();
        CreateMap<ViewListTheatersRequest, ViewTheaterQuery>();
    }
}
