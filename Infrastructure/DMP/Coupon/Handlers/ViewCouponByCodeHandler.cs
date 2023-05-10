using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Coupon.Handlers;
using Application.DMP.Coupon.Queries;
using Application.DMP.Coupon.Services;
using Application.DTO.DMP.Coupon.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Coupon.Handlers;
[ExcludeFromCodeCoverage]
public class ViewCouponByCodeHandler : IViewCouponByCodeHandler
{
    private readonly ICouponManagementService _couponManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewCouponByCodeHandler(ILoggerService loggerService, IMapper mapper, ICouponManagementService couponManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _couponManagementService = couponManagementService;
    }

    public async Task<Result<ViewCouponResponse>> Handle(ViewCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _couponManagementService.ViewCouponByCodeAsync(request.Code, cancellationToken);
            if (result.Succeeded)
                return Result<ViewCouponResponse>.Succeed(data: result.Data);
            return Result<ViewCouponResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateCouponHandler));
            Console.WriteLine(e);
            return Result<ViewCouponResponse>.Fail(Constants.CommitFailed);
        }
    }
}
