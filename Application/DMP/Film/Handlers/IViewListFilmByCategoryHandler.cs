using Application.Common.Models;
using Application.DMP.Film.Queries;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Film.Handlers;
public interface IViewListFilmByCategoryHandler: IRequestHandler<ViewListFilmByCategoryQuery, Result<PaginationBaseResponse<ViewFilmResponse>>>
{
}
