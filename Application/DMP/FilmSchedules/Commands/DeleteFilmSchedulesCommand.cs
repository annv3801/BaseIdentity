using Application.Common.Models;
using Application.DMP.FilmSchedules.Commons;
using MediatR;

namespace Application.DMP.FilmSchedules.Commands;

public class DeleteFilmSchedulesCommand : IRequest<Result<FilmSchedulesResult>>
{
    public Guid Id { get; set; }
}