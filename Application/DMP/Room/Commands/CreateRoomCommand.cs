using Application.Common.Models;
using Application.DMP.Room.Commons;
using Application.DTO.DMP.Room.Requests;
using MediatR;

namespace Application.DMP.Room.Commands;

public class CreateRoomCommand : CreateRoomRequest, IRequest<Result<RoomResult>>
{
    
}