using Application.Common.Models;
using Application.DMP.Room.Queries;
using Application.DTO.DMP.Room.Responses;
using MediatR;

namespace Application.DMP.Room.Handlers;
public interface IViewRoomHandler: IRequestHandler<ViewRoomQuery, Result<ViewRoomResponse>>
{
    
}
