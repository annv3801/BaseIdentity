using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Coupon.Commands;
using Application.DMP.Coupon.Commons;
using Application.DMP.Coupon.Handlers;
using Application.DMP.Coupon.Services;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Commons;
using Application.DMP.Slider.Handlers;
using Application.DMP.Slider.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Coupon.Handlers;
[ExcludeFromCodeCoverage]
public class CreateCouponHandler : ICreateCouponHandlers
{
    private readonly ICouponManagementService _couponManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public CreateCouponHandler(ILoggerService loggerService, IMapper mapper, ICouponManagementService couponManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _couponManagementService = couponManagementService;
    }

    public async Task<Result<CouponResult>> Handle(CreateCouponCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var coupon = _mapper.Map<Domain.Entities.DMP.Coupon>(command);
            var result = await _couponManagementService.CreateCouponAsync(coupon, cancellationToken);
            if (result.Succeeded)
                return Result<CouponResult>.Succeed(data: result.Data);
            return Result<CouponResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateCouponHandler));
            Console.WriteLine(e);
            return Result<CouponResult>.Fail(Constants.CommitFailed);
        }
    }
}
