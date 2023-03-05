using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Seats
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}