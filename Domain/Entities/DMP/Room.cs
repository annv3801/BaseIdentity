using Domain.Common;
using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities.DMP;

public class Room : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DMPStatus? Status { get; set; }
    public Guid TheaterId { get; set; }
    public Theater Theater { get; set; }
}