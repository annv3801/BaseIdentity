using Application.Common;
using Application.DMP.Room.Commands;
using Application.DMP.Room.Queries;
using Application.DTO.DMP.Room.Requests;
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
[SwaggerTag(Constants.SwaggerTags.Room)]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;

    public RoomController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.Room.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateRoomAsync(CreateRoomRequest createRoomRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createRoomCommand = _mapper.Map<CreateRoomCommand>(createRoomRequest);
            var result = await _mediator.Send(createRoomCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoomAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Room.View)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewRoomQuery() {Id = roomId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewRoomAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpDelete]
    [LogAction]
    [Route(Common.Url.DMP.Room.Delete)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> DeleteRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new DeleteRoomCommand() {Id = roomId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteRoomAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    [LogAction]
    [Route(Common.Url.DMP.Room.Update)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> UpdateRoomAsync(Guid roomId, UpdateRoomRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<UpdateRoomCommand>(request);
            command.Id = roomId;
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateRoomAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Room.ViewList)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListRoomsAsync([FromQuery] ViewListRoomsRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListRoomsQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListRoomsAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}