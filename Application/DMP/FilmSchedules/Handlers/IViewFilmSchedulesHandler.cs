using Application.Common.Models;
using Application.DMP.FilmSchedules.Queries;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.DMP.Theater.Responses;
using MediatR;

namespace Application.DMP.FilmSchedules.Handlers;
public interface IViewFilmSchedulesHandler: IRequestHandler<ViewFilmSchedulesQuery, Result<ViewFilmSchedulesResponse>>
{
    
}
