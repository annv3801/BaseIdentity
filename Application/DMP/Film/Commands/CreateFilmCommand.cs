using Application.Common.Models;
using Application.DMP.Film.Commons;
using Application.DTO.DMP.Film.Requests;
using MediatR;

namespace Application.DMP.Film.Commands;

public class CreateFilmCommand : CreateFilmRequest, IRequest<Result<FilmResult>>
{
    
}