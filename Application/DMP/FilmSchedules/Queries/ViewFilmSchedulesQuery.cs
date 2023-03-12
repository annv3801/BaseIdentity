using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.FilmSchedules.Responses;
using MediatR;

namespace Application.DMP.FilmSchedules.Queries;
[ExcludeFromCodeCoverage]
public class ViewFilmSchedulesQuery : IRequest<Result<ViewFilmSchedulesResponse>>
{
    public Guid Id { get; set; }
}
