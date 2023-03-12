using Application.DMP.FilmSchedules.Commands;
using Application.DMP.FilmSchedules.Queries;
using Application.DTO.DMP.FilmSchedules.Requests;
using Application.DTO.DMP.FilmSchedules.Responses;
using AutoMapper;

namespace Infrastructure.DMP.FilmSchedules.Mappings;
public class FilmSchedulesProfile : Profile
{
    public FilmSchedulesProfile()
    {
        CreateMap<CreateFilmSchedulesRequest, CreateFilmSchedulesCommand>();

        CreateMap<CreateFilmSchedulesCommand, Domain.Entities.DMP.FilmSchedules>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.FilmId, o => o.MapFrom(s => s.FilmId))
            .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
            .ForMember(d => d.StartTime, o => o.MapFrom(s => s.StartTime))
            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime));
        CreateMap<Domain.Entities.DMP.FilmSchedules, ViewFilmSchedulesResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.FilmId, o => o.MapFrom(s => s.FilmId))
                    .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
                    .ForMember(d => d.StartTime, o => o.MapFrom(s => s.StartTime))
                    .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime));
        CreateMap<UpdateFilmSchedulesRequest, UpdateFilmSchedulesCommand>();
        CreateMap<UpdateFilmSchedulesCommand, Domain.Entities.DMP.FilmSchedules>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.FilmId, o => o.MapFrom(s => s.FilmId))
            .ForMember(d => d.RoomId, o => o.MapFrom(s => s.RoomId))
            .ForMember(d => d.StartTime, o => o.MapFrom(s => s.StartTime))
            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime));
        CreateMap<ViewListFilmSchedulesRequest, ViewListFilmSchedulesQuery>();
        CreateMap<ViewListFilmSchedulesRequest, ViewFilmSchedulesQuery>();
    }
}
