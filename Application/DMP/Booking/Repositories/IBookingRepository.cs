using Application.Common.Interfaces;
using Domain.Entities.DMP;

namespace Application.DMP.Booking.Repositories;
public interface IBookingRepository : IRepository<Domain.Entities.DMP.Booking>
{
    Task<Domain.Entities.DMP.Booking?> GetBookingAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default(CancellationToken));
}
