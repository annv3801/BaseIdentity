using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Coupon.Queries;
[ExcludeFromCodeCoverage]
public class ViewListCouponsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewCouponResponse>>>
{
}
