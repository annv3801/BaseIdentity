using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Seat.Requests;
[ExcludeFromCodeCoverage]
public class UpdateSeatRequest
{
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public int Type { get; set; }
    public DMPStatus Status { get; set; }
}
