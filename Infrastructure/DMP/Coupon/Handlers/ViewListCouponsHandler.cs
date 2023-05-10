using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Coupon.Handlers;
using Application.DMP.Coupon.Queries;
using Application.DMP.Coupon.Services;
using Application.DMP.Slider.Handlers;
using Application.DMP.Slider.Queries;
using Application.DMP.Slider.Services;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Coupon.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListCouponsHandler : IViewListCouponsHandler
{
    private readonly ICouponManagementService _couponManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListCouponsHandler(ILoggerService loggerService, IMapper mapper, ICouponManagementService couponManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _couponManagementService = couponManagementService;
    }

    public async Task<Result<PaginationBaseResponse<ViewCouponResponse>>> Handle(ViewListCouponsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.ViewListCouponsAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewCouponResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewCouponResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListCouponsHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewCouponResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
