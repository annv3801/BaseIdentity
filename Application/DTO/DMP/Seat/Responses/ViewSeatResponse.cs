using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.Seat.Responses;
[ExcludeFromCodeCoverage]
public class ViewSeatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid RoomId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
}
