using Application.Common.Models;
using Application.DMP.Coupon.Queries;
using Application.DTO.DMP.Coupon.Responses;
using MediatR;

namespace Application.DMP.Coupon.Handlers;
public interface IViewCouponHandler: IRequestHandler<ViewCouponQuery, Result<ViewCouponResponse>>
{
    
}
