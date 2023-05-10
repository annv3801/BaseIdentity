using Application.Common;
using Application.DMP.Coupon.Commands;
using Application.DMP.Coupon.Queries;
using Application.DMP.Film.Commands;
using Application.DMP.Film.Queries;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Coupon.Requests;
using Application.DTO.DMP.Film.Requests;
using Application.DTO.DMP.Slider.Requests;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Common.Attributes.ActionLog;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Areas.DMP.Controllers;

[ApiController]
[Area(Common.Url.Areas.DMP)]
[SwaggerTag(Constants.SwaggerTags.Coupon)]
public class CouponController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;

    public CouponController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.Coupon.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateCouponAsync([FromForm]CreateCouponRequest createCouponRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createCouponCommand = _mapper.Map<CreateCouponCommand>(createCouponRequest);
            var result = await _mediator.Send(createCouponCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateCouponAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Coupon.View)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewCouponAsync(Guid couponId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewCouponQuery() {Id = couponId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewCouponAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Coupon.ViewByCode)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewCouponByCodeAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewCouponByCodeQuery() {Code = code}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewCouponByCodeAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Coupon.ViewList)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListCouponsAsync([FromQuery] ViewListCouponsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListCouponsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListCouponsAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}