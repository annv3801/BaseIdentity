using Application.Common.Models;
using Application.DMP.Film.Commands;
using Application.DMP.Film.Commons;
using MediatR;

namespace Application.DMP.Film.Handlers;

public interface ICreateFilmHandlers : IRequestHandler<CreateFilmCommand, Result<FilmResult>>
{
    
}