using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Theater
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public Account CreatedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public Account ModifiedBy { get; set; }
}