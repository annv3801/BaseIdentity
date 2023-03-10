using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Domain.Enums;

namespace Application.DTO.DMP.Category.Responses;
[ExcludeFromCodeCoverage]
public class ViewCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public int Status { get; set; }
}
