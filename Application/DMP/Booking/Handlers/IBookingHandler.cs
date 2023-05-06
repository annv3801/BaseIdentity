using Application.Common.Models;
using Application.DMP.Booking.Commands;
using Application.DMP.Booking.Commons;
using MediatR;

namespace Application.DMP.Booking.Handlers;

public interface IBookingHandlers : IRequestHandler<BookingCommand, Result<BookingResult>>
{
    
}