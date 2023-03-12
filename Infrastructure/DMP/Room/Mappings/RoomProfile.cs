using Application.DMP.Room.Commands;
using Application.DMP.Room.Queries;
using Application.DTO.DMP.Room.Requests;
using Application.DTO.DMP.Room.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Room.Mappings;
public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreateRoomRequest, CreateRoomCommand>();

        CreateMap<CreateRoomCommand, Domain.Entities.DMP.Room>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.TheaterId, o => o.MapFrom(s => s.TheaterId))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<Domain.Entities.DMP.Room, ViewRoomResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.TheaterId, o => o.MapFrom(s => s.TheaterId))
                    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<UpdateRoomRequest, UpdateRoomCommand>();
        CreateMap<UpdateRoomCommand, Domain.Entities.DMP.Room>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.TheaterId, o => o.MapFrom(s => s.TheaterId))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<ViewListRoomsRequest, ViewListRoomsQuery>();
        CreateMap<ViewListRoomsRequest, ViewRoomQuery>();
    }
}
