using Application.Common.Models;
using Application.DMP.Coupon.Commands;
using Application.DMP.Coupon.Commons;
using Application.DMP.Film.Commands;
using Application.DMP.Film.Commons;
using MediatR;

namespace Application.DMP.Coupon.Handlers;

public interface ICreateCouponHandlers : IRequestHandler<CreateCouponCommand, Result<CouponResult>>
{
    
}