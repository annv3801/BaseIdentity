using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.FilmSchedules.Queries;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesByTimeQuery : IRequest<Result<List<TheaterScheduleResponse>>>
{
    public DateTime Date { get; set; }
}
