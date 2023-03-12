using Application.Common.Models;
using Application.DMP.FilmSchedules.Queries;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.FilmSchedules.Handlers;
public interface IViewListFilmSchedulesHandler: IRequestHandler<ViewListFilmSchedulesQuery, Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>>
{
    
}
