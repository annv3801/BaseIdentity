using Application.Common.Models;
using Application.DMP.Room.Commons;
using Application.DMP.Theater.Commons;
using MediatR;

namespace Application.DMP.Room.Commands;

public class DeleteRoomCommand : IRequest<Result<RoomResult>>
{
    public Guid Id { get; set; }
}