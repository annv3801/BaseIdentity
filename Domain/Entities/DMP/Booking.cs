using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Booking : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; }
    public double Total { get; set; }
}