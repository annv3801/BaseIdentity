using System.Diagnostics.CodeAnalysis;
using Application.DTO.Pagination.Requests;

namespace Application.DTO.DMP.FilmSchedules.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesByTimeRequest
{
    public DateTime Date { get; set; }
}
