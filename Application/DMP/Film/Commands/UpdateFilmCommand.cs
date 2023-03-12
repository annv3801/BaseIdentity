using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Film.Commons;
using Application.DTO.DMP.Film.Requests;
using MediatR;

namespace Application.DMP.Film.Commands;
[ExcludeFromCodeCoverage]
public class UpdateFilmCommand : UpdateFilmRequest, IRequest<Result<FilmResult>>
{
    public Guid Id { get; set; }
}
