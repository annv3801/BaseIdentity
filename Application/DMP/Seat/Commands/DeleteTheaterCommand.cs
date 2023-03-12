using Application.Common.Models;
using Application.DMP.Seat.Commons;
using MediatR;

namespace Application.DMP.Seat.Commands;

public class DeleteSeatCommand : IRequest<Result<SeatResult>>
{
    public Guid Id { get; set; }
}