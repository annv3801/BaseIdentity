using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.FilmSchedules.Requests;
[ExcludeFromCodeCoverage]
public class UpdateFilmSchedulesRequest
{
    public Guid FilmId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
