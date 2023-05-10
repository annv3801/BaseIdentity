using Application.Common.Interfaces;
using Application.DMP.Coupon.Repositories;
using Application.DMP.Slider.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Coupon.Repositories;
public class CouponRepository : Repository<Domain.Entities.DMP.Coupon>, ICouponRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public CouponRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Domain.Entities.DMP.Coupon?> GetCouponAsync(Guid couponId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Coupons.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == couponId, cancellationToken);
    }

    public async Task<Domain.Entities.DMP.Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.Coupons.AsSplitQuery().FirstOrDefaultAsync(r => r.Code == code, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Coupon>> ViewListCouponsAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Coupons
            .AsSplitQuery()
            .AsQueryable();
    }
}
