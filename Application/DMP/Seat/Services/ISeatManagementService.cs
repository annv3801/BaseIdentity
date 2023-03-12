using Application.Common.Models;
using Application.DMP.Seat.Commons;
using Application.DMP.Seat.Queries;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Seat.Services;
public interface ISeatManagementService
{
    Task<Result<SeatResult>> CreateSeatAsync(Domain.Entities.DMP.Seat seat, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewSeatResponse>> ViewSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SeatResult>> DeleteSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<SeatResult>> UpdateSeatAsync(Domain.Entities.DMP.Seat seat, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsAsync(ViewListSeatsQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
