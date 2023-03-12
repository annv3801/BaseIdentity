using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Seat.Commons;
using Application.DTO.DMP.Seat.Requests;
using Application.DTO.DMP.Theater.Requests;
using MediatR;

namespace Application.DMP.Seat.Commands;
[ExcludeFromCodeCoverage]
public class UpdateSeatCommand : UpdateSeatRequest, IRequest<Result<SeatResult>>
{
    public Guid Id { get; set; }
}
