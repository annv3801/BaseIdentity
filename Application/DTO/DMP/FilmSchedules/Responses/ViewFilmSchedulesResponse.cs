using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.FilmSchedules.Responses;
[ExcludeFromCodeCoverage]
public class ViewFilmSchedulesResponse
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public Guid RoomId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
