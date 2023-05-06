using Application.Common.Models;
using Application.DMP.Booking.Commons;
using Application.DTO.DMP.Booking.Requests;
using MediatR;

namespace Application.DMP.Booking.Commands;

public class BookingCommand : BookingRequest, IRequest<Result<BookingResult>>
{
    
}