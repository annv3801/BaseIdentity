﻿using Application.Common.Models;
using Application.DMP.FilmSchedules.Commands;
using Application.DMP.FilmSchedules.Commons;
using MediatR;

namespace Application.DMP.FilmSchedules.Handlers;

public interface IDeleteFilmSchedulesHandler : IRequestHandler<DeleteFilmSchedulesCommand, Result<FilmSchedulesResult>>
{
    
}