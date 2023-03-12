using Application.Common.Models;
using Application.DMP.Seat.Commands;
using Application.DMP.Seat.Commons;
using MediatR;

namespace Application.DMP.Seat.Handlers;
public interface IUpdateSeatHandler: IRequestHandler<UpdateSeatCommand, Result<SeatResult>>
{
}