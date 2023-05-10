using Application.Common.Models;
using Application.DMP.Coupon.Queries;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Coupon.Handlers;
public interface IViewListCouponsHandler: IRequestHandler<ViewListCouponsQuery, Result<PaginationBaseResponse<ViewCouponResponse>>>
{
    
}
