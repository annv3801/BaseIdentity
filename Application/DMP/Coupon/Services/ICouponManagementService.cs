using Application.Common.Models;
using Application.DMP.Coupon.Commons;
using Application.DMP.Coupon.Queries;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Queries;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Coupon.Services;
public interface ICouponManagementService
{
    Task<Result<CouponResult>> CreateCouponAsync(Domain.Entities.DMP.Coupon coupon, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCouponResponse>> ViewCouponAsync(Guid couponId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewCouponResponse>>> ViewListCouponsAsync(ViewListCouponsQuery query, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCouponResponse>> ViewCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken));
}
