using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class FilmSchedules
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Films Films { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}