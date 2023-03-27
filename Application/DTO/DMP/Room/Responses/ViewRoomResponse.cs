using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

namespace Application.DTO.DMP.Room.Responses;
[ExcludeFromCodeCoverage]
public class ViewRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DMPStatus? Status { get; set; }
    public Guid TheaterId { get; set; }
    public string TheaterName { get; set; }
    public string TheaterAddress { get; set; }
}
