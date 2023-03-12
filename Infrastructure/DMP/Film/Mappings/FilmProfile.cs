using Application.DMP.Film.Commands;
using Application.DMP.Film.Queries;
using Application.DTO.DMP.Film.Requests;
using Application.DTO.DMP.Film.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Film.Mappings;
public class FilmProfile : Profile
{
    public FilmProfile()
    {
        CreateMap<CreateFilmRequest, CreateFilmCommand>();

        CreateMap<CreateFilmCommand, Domain.Entities.DMP.Film>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
            .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
            .ForMember(d => d.Actor, o => o.MapFrom(s => s.Actor))
            .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
            .ForMember(d => d.Premiere, o => o.MapFrom(s => s.Premiere))
            .ForMember(d => d.Duration, o => o.MapFrom(s => s.Duration))
            .ForMember(d => d.Language, o => o.MapFrom(s => s.Language))
            .ForMember(d => d.Rated, o => o.MapFrom(s => s.Rated));
        CreateMap<Domain.Entities.DMP.Film, ViewFilmResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
            .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
            .ForMember(d => d.Actor, o => o.MapFrom(s => s.Actor))
            .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
            .ForMember(d => d.Premiere, o => o.MapFrom(s => s.Premiere))
            .ForMember(d => d.Duration, o => o.MapFrom(s => s.Duration))
            .ForMember(d => d.Language, o => o.MapFrom(s => s.Language))
            .ForMember(d => d.Rated, o => o.MapFrom(s => s.Rated));
        CreateMap<UpdateFilmRequest, UpdateFilmCommand>();
        CreateMap<UpdateFilmCommand, Domain.Entities.DMP.Film>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
            .ForMember(d => d.CategoryId, o => o.MapFrom(s => s.CategoryId))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Director, o => o.MapFrom(s => s.Director))
            .ForMember(d => d.Actor, o => o.MapFrom(s => s.Actor))
            .ForMember(d => d.Genre, o => o.MapFrom(s => s.Genre))
            .ForMember(d => d.Premiere, o => o.MapFrom(s => s.Premiere))
            .ForMember(d => d.Duration, o => o.MapFrom(s => s.Duration))
            .ForMember(d => d.Language, o => o.MapFrom(s => s.Language))
            .ForMember(d => d.Rated, o => o.MapFrom(s => s.Rated));
        CreateMap<ViewListFilmsRequest, ViewListFilmsQuery>();
        CreateMap<ViewListFilmsRequest, ViewFilmQuery>();
    }
}
