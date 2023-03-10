using Domain.Common;
using Domain.Enums;

namespace Domain.Entities.DMP;

public class Category : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public DMPStatus Status { get; set; }
}