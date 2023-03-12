using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Commons;
using Application.DTO.DMP.FilmSchedules.Requests;
using MediatR;

namespace Application.DMP.FilmSchedules.Commands;
[ExcludeFromCodeCoverage]
public class UpdateFilmSchedulesCommand : UpdateFilmSchedulesRequest, IRequest<Result<FilmSchedulesResult>>
{
    public Guid Id { get; set; }
}
