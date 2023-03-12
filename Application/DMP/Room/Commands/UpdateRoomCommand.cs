using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Room.Commons;
using Application.DMP.Theater.Commons;
using Application.DTO.DMP.Room.Requests;
using Application.DTO.DMP.Theater.Requests;
using MediatR;

namespace Application.DMP.Room.Commands;
[ExcludeFromCodeCoverage]
public class UpdateRoomCommand : UpdateRoomRequest, IRequest<Result<RoomResult>>
{
    public Guid Id { get; set; }
}
