using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Tickets
{
    public Guid Id { get; set; }
    public Guid ScheduleId { get; set; }
    public FilmSchedules FilmSchedules { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}