using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.Room.Responses;
[ExcludeFromCodeCoverage]
public class ViewRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public Guid TheaterId { get; set; }
}
