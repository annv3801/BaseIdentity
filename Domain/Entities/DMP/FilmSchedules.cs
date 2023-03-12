using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class FilmSchedules : AuditableEntity
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Films Films { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}