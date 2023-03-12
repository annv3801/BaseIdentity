using Application.Common.Models;
using Application.DMP.Category.Queries;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Queries;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Film.Services;
public interface IFilmManagementService
{
    Task<Result<FilmResult>> CreateFilmAsync(Domain.Entities.DMP.Film film, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewFilmResponse>> ViewFilmAsync(Guid filmId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<FilmResult>> UpdateFilmAsync(Domain.Entities.DMP.Film film, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewFilmResponse>>> ViewListFilmsAsync(ViewListFilmsQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
