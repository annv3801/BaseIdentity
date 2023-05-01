using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.Booking.Requests;
[ExcludeFromCodeCoverage]
public class BookingRequest
{
    public Guid AccountId { get; set; }
    public Guid ScheduleId { get; set; }
    public List<Guid> SeatId { get; set; }
    public double Total { get; set; }
}
