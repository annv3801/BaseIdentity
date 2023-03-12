using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.FilmSchedules.Queries;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>>
{
}
