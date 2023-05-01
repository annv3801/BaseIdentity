using Application.Common;
using Application.DMP.FilmSchedules.Commands;
using Application.DMP.FilmSchedules.Queries;
using Application.DTO.DMP.FilmSchedules.Requests;
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
[SwaggerTag(Constants.SwaggerTags.FilmSchedules)]
public class FilmSchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILoggerService _loggerService;

    public FilmSchedulesController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _loggerService = loggerService;
    }

    
    [HttpPost]
    [LogAction]
    // [Permissions(Permissions = new[] {Constants.Permissions.SysAdmin, Constants.Permissions.TenantAdmin})]
    [Route(Common.Url.DMP.FilmSchedules.Create)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> CreateFilmSchedulesAsync(CreateFilmSchedulesRequest createFilmSchedulesRequest, CancellationToken cancellationToken)
    {
        try
        {
            var createFilmSchedulesCommand = _mapper.Map<CreateFilmSchedulesCommand>(createFilmSchedulesRequest);
            var result = await _mediator.Send(createFilmSchedulesCommand, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: new {result.Data}));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateFilmSchedulesAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.FilmSchedules.View)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> ViewFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new ViewFilmSchedulesQuery() {Id = filmScheduleId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewFilmSchedulesAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpDelete]
    [LogAction]
    [Route(Common.Url.DMP.FilmSchedules.Delete)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> DeleteFilmSchedulesAsync(Guid filmScheduleId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var result = await _mediator.Send(new DeleteFilmSchedulesCommand() {Id = filmScheduleId}, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteFilmSchedulesAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpPut]
    [LogAction]
    [Route(Common.Url.DMP.FilmSchedules.Update)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    public async Task<IActionResult> UpdateFilmSchedulesAsync(Guid filmScheduleId, UpdateFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var command = _mapper.Map<UpdateFilmSchedulesCommand>(request);
            command.Id = filmScheduleId;
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse());
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateFilmSchedulesAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.FilmSchedules.ViewList)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListFilmSchedulessAsync([FromQuery] ViewListFilmSchedulesRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListFilmSchedulesQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulessAsync));
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet]
    [LogAction]
    [Route(Common.Url.DMP.FilmSchedules.ViewByFilmId)]
    [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
    [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
    [Produces(Constants.MimeTypes.Application.Json)]
    // [Cached]
    public async Task<IActionResult>? ViewListFilmSchedulessByTimeAsync([FromQuery] ViewListFilmSchedulesByTimeRequest request, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var query = _mapper.Map<ViewListFilmSchedulesByTimeQuery>(request);
            var result = await _mediator.Send(query, cancellationToken);
            if (result.Succeeded)
                return Ok(new SuccessResponse(data: result.Data));
            return Accepted(new FailureResponse(result.Errors));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulessByTimeAsync));
            Console.WriteLine(e);
            throw;
        }
    }
}