using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.Room.Requests;
[ExcludeFromCodeCoverage]
public class UpdateRoomRequest
{
    public string Name { get; set; }
    public Guid TheaterId { get; set; }
    public int Status { get; set; }
}
