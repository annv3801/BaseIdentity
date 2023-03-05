using Domain.Common;

namespace Domain.Entities.DMP;

public class Category : AuditableEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public bool Status { get; set; }
}