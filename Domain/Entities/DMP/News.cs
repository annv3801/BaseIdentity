using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities.DMP;

public class News : AuditableEntity
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public string? Image { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DMPStatus Status { get; set; }
}