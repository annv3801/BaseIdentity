using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Seat.Responses;
[ExcludeFromCodeCoverage]
public class ViewSeatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string RoomName { get; set; }
    public string TheaterName { get; set; }
    public int Type { get; set; }
    public DMPStatus Status { get; set; }
}
