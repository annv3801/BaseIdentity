using Application.Common.Models;
using Application.DMP.Theater.Commons;
using Application.DMP.Theater.Queries;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Theater.Services;
public interface ITheaterManagementService
{
    Task<Result<TheaterResult>> CreateTheaterAsync(Domain.Entities.DMP.Theater theater, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewTheaterResponse>> ViewTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TheaterResult>> DeleteTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TheaterResult>> UpdateTheaterAsync(Domain.Entities.DMP.Theater theater, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewTheaterResponse>>> ViewListTheatersAsync(ViewListTheatersQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
