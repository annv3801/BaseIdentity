using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Theater.Responses;
[ExcludeFromCodeCoverage]
public class ViewTheaterResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DMPStatus Status { get; set; }
}
