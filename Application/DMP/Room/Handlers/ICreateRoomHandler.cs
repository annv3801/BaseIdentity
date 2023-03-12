using Application.Common.Models;
using Application.DMP.Room.Commands;
using Application.DMP.Room.Commons;
using MediatR;

namespace Application.DMP.Room.Handlers;

public interface ICreateRoomHandlers : IRequestHandler<CreateRoomCommand, Result<RoomResult>>
{
    
}