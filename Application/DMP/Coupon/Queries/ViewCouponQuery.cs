using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Coupon.Responses;
using MediatR;

namespace Application.DMP.Coupon.Queries;
[ExcludeFromCodeCoverage]
public class ViewCouponQuery : IRequest<Result<ViewCouponResponse>>
{
    public Guid Id { get; set; }
}
