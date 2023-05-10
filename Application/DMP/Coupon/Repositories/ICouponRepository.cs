using Application.Common.Interfaces;

namespace Application.DMP.Coupon.Repositories;
public interface ICouponRepository : IRepository<Domain.Entities.DMP.Coupon>
{
    Task<Domain.Entities.DMP.Coupon?> GetCouponAsync(Guid couponId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Coupon>> ViewListCouponsAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<Domain.Entities.DMP.Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken));
}
