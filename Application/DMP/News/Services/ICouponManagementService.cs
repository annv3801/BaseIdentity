using Application.Common.Models;
using Application.DMP.Coupon.Commons;
using Application.DMP.Coupon.Queries;
using Application.DMP.News.Commons;
using Application.DMP.News.Queries;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.News.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.News.Services;
public interface INewsManagementService
{
    Task<Result<NewsResult>> CreateNewsAsync(Domain.Entities.DMP.News news, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewNewsResponse>> ViewNewsAsync(Guid newsId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewNewsResponse>>> ViewListNewsAsync(ViewListNewsQuery query, CancellationToken cancellationToken = default(CancellationToken));
}
