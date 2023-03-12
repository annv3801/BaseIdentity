using Application.Common;
using Application.DMP.Theater.Commands;
using Application.DMP.Theater.Queries;
using Application.DTO.DMP.Theater.Requests;
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
[SwaggerTag(Constants.SwaggerTags.Theater)]
public class TheaterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;

    public TheaterController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.Theater.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateTheaterAsync(CreateTheaterRequest createTheaterRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createTheaterCommand = _mapper.Map<CreateTheaterCommand>(createTheaterRequest);
            var result = await _mediator.Send(createTheaterCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateTheaterAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Theater.View)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewTheaterQuery() {Id = theaterId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewTheaterAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpDelete]
    [LogAction]
    [Route(Common.Url.DMP.Theater.Delete)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> DeleteTheaterAsync(Guid theaterId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new DeleteTheaterCommand() {Id = theaterId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteTheaterAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    [LogAction]
    [Route(Common.Url.DMP.Theater.Update)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> UpdateTheaterAsync(Guid theaterId, UpdateTheaterRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<UpdateTheaterCommand>(request);
            command.Id = theaterId;
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateTheaterAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.Theater.ViewList)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListTheatersAsync([FromQuery] ViewListTheatersRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListTheatersQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListTheatersAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}