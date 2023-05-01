using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Seat.Queries;
[ExcludeFromCodeCoverage]
public class ViewListSeatsByScheduleQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewSeatResponse>>>
{
    public Guid ScheduleId { get; set; }
}
