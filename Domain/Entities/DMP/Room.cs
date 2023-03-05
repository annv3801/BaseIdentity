using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool? Status { get; set; }
    public Guid TheaterId { get; set; }
    public Theater Theater { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}