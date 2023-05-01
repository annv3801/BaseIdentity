namespace Application.DTO.DMP.FilmSchedules.Responses;

public class ScheduleResponse
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public string FilmName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}