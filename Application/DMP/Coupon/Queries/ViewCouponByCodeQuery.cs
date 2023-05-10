using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Coupon.Responses;
using MediatR;

namespace Application.DMP.Coupon.Queries;
[ExcludeFromCodeCoverage]
public class ViewCouponByCodeQuery : IRequest<Result<ViewCouponResponse>>
{
    public string Code { get; set; }
}
