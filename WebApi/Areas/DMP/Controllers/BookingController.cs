using Application.Common;
using Application.Common.Interfaces;
using Application.DMP.Booking.Commands;
using Application.DTO.DMP.Booking.Requests;
using Application.DTO.VnPay;
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
[SwaggerTag(Constants.SwaggerTags.Category)]
public class BookingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;
    private readonly IVnPayService _vnPayService;
    public BookingController(IMediator mediator, IMapper mapper, ILoggerService loggerService, IVnPayService vnPayService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
        _vnPayService = vnPayService;
    }
    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.Booking.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> BookingAsync(BookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createCategoryCommand = _mapper.Map<BookingCommand>(bookingRequest);
            var result = await _mediator.Send(createCategoryCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(BookingAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpPost]
    [Route(Common.Url.DMP.Booking.PaymentVnpay)]
    public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
    {
        var test = new PaymentInformationModel()
        {
            OrderType = "other",
            Amount = 120000,
            OrderDescription = "Test 1",
            Name = "An"
        };
        var url = _vnPayService.CreatePaymentUrl(test);
        return Redirect(url);
    }
}