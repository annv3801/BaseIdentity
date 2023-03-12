using Application.Common.Models;
using Application.DMP.FilmSchedules.Commons;
using Application.DTO.DMP.FilmSchedules.Requests;
using MediatR;

namespace Application.DMP.FilmSchedules.Commands;

public class CreateFilmSchedulesCommand : CreateFilmSchedulesRequest, IRequest<Result<FilmSchedulesResult>>
{
    
}