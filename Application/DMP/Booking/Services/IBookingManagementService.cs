using Application.Common.Models;
using Application.DMP.Booking.Commons;
using Application.DTO.DMP.Booking.Requests;
using Application.DTO.VnPay;

namespace Application.DMP.Booking.Services;
public interface IBookingManagementService
{
    Task<Result<BookingResult>> BookingAsync(BookingRequest request, CancellationToken cancellationToken);

}
