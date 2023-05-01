using Application.Common.Models;
using Application.DMP.Seat.Queries;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Seat.Handlers;
public interface IViewListSeatsByScheduleHandler: IRequestHandler<ViewListSeatsByScheduleQuery, Result<PaginationBaseResponse<ViewSeatResponse>>>
{
    
}
