using System.Diagnostics.CodeAnalysis;
using Application.DTO.Pagination.Requests;

namespace Application.DTO.DMP.Seat.Requests;
[ExcludeFromCodeCoverage]
public class ViewListSeatsByScheduleRequest : PaginationBaseRequest
{
    public Guid ScheduleId { get; set; }
}