using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.DMP;

public class Theater : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DMPStatus Status { get; set; }
}