using Application.DMP.Seat.Commands;
using Application.DMP.Seat.Queries;
using Application.DTO.DMP.Seat.Requests;
using Application.DTO.DMP.Seat.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Seat.Mappings;
public class SeatProfile : Profile
{
    public SeatProfile()
    {
        CreateMap<CreateSeatRequest, CreateSeatCommand>();

        CreateMap<CreateSeatCommand, Domain.Entities.DMP.Seat>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<Domain.Entities.DMP.Seat, ViewSeatResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<UpdateSeatRequest, UpdateSeatCommand>();
        CreateMap<UpdateSeatCommand, Domain.Entities.DMP.Seat>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<ViewListSeatsRequest, ViewListSeatsQuery>();
        CreateMap<ViewListSeatsRequest, ViewSeatQuery>();
    }
}
