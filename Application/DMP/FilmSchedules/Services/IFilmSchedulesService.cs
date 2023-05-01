using Application.Common.Models;
using Application.DMP.FilmSchedules.Commons;
using Application.DMP.FilmSchedules.Queries;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.FilmSchedules.Services;
public interface IFilmSchedulesManagementService
{
    Task<Result<FilmSchedulesResult>> CreateFilmSchedulesAsync(Domain.Entities.DMP.FilmSchedule filmSchedule, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewFilmSchedulesResponse>> ViewFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmSchedulesResult>> DeleteFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmSchedulesResult>> UpdateFilmSchedulesAsync(Domain.Entities.DMP.FilmSchedule filmSchedule, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>> ViewListFilmSchedulesAsync(ViewListFilmSchedulesQuery query, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<List<TheaterScheduleResponse>>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery query, CancellationToken cancellationToken = default(CancellationToken));
}
