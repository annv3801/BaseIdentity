using Application.Common;
using Application.DMP.Seat.Commands;
using Application.DMP.Seat.Queries;
using Application.DTO.DMP.Seat.Requests;
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
[SwaggerTag(Constants.SwaggerTags.Seat)]
public class SeatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;

    public SeatController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.Seat.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateSeatAsync(CreateSeatRequest createSeatRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createSeatCommand = _mapper.Map<CreateSeatCommand>(createSeatRequest);
            var result = await _mediator.Send(createSeatCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Seat.View)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewSeatQuery() {Id = seatId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpDelete]
    [LogAction]
    [Route(Common.Url.DMP.Seat.Delete)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> DeleteSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new DeleteSeatCommand() {Id = seatId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    [LogAction]
    [Route(Common.Url.DMP.Seat.Update)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> UpdateSeatAsync(Guid seatId, UpdateSeatRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<UpdateSeatCommand>(request);
            command.Id = seatId;
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateSeatAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Seat.ViewList)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListSeatsAsync([FromQuery] ViewListSeatsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListSeatsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSeatsAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Seat.ViewListBySchedule)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListSeatsByScheduleAsync([FromQuery] ViewListSeatsByScheduleRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListSeatsByScheduleQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSeatsAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}