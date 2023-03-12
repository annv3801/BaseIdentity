using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Theater.Requests;
[ExcludeFromCodeCoverage]
public class UpdateTheaterRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public int Status { get; set; }
}
