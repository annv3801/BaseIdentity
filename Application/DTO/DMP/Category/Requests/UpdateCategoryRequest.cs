using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Category.Requests;
[ExcludeFromCodeCoverage]
public class UpdateCategoryRequest
{
    public string Name { get; set; } = "";
    public string ShortenUrl { get; set; } = "";
    public DMPStatus Status { get; set; } = DMPStatus.Active;
}
