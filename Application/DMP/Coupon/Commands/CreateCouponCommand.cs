using Application.Common.Models;
using Application.DMP.Coupon.Commons;
using Application.DTO.DMP.Coupon.Requests;
using MediatR;

namespace Application.DMP.Coupon.Commands;

public class CreateCouponCommand : CreateCouponRequest, IRequest<Result<CouponResult>>
{
    
}