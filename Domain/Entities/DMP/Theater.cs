using Domain.Common;
using Domain.Entities.Identity;

namespace Domain.Entities.DMP;

public class Theater : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool Status { get; set; }
}