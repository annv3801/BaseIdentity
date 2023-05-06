using Application.Common.Interfaces;
using Application.DMP.Booking.Repositories;
using Domain.Entities.DMP;
using Infrastructure.Common.Repositories;

namespace Infrastructure.DMP.Booking.Repositories;
public class BookingRepository : Repository<Domain.Entities.DMP.Booking>, IBookingRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public BookingRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<Domain.Entities.DMP.Booking?> GetBookingAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken))
    {
        throw new NotImplementedException();
    }

    public async Task AddRangeAsync(IEnumerable<BookingDetail> bookingDetails, CancellationToken cancellationToken = default)
    {
        await _applicationDbContext.Set<BookingDetail>().AddRangeAsync(bookingDetails, cancellationToken);
    }
}
