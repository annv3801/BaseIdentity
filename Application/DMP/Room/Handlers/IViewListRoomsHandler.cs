using Application.Common.Models;
using Application.DMP.Room.Queries;
using Application.DTO.DMP.Room.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Room.Handlers;
public interface IViewListRoomsHandler: IRequestHandler<ViewListRoomsQuery, Result<PaginationBaseResponse<ViewRoomResponse>>>
{
    
}
