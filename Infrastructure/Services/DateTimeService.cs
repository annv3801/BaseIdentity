using Domain.Interfaces;

namespace Infrastructure.Services;
public class DateTimeService : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}
