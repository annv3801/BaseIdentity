using Application.Common.Models;
using Application.DMP.Seat.Commons;
using Application.DTO.DMP.Seat.Requests;
using MediatR;

namespace Application.DMP.Seat.Commands;

public class CreateSeatCommand : CreateSeatRequest, IRequest<Result<SeatResult>>
{
    
}