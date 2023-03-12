using Application.Common.Models;
using Application.DMP.Theater.Queries;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Theater.Handlers;
public interface IViewListTheatersHandler: IRequestHandler<ViewListTheatersQuery, Result<PaginationBaseResponse<ViewTheaterResponse>>>
{
    
}
