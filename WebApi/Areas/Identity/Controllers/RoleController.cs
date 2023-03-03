using Application.Common;
using Application.DTO.Role.Requests;
using Application.Identity.Role.Commands;
using Application.Identity.Role.Queries;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Common.Attributes.ActionLog;
using Infrastructure.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Areas.Identity.Controllers
{
    /// <inheritdoc />
    [ApiController]
    [Area(Common.Url.Areas.Identity)]
    [SwaggerTag(Constants.SwaggerTags.Role)]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILoggerService _loggerService;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="mapper"></param>
        /// <param name="loggerService"></param>
        public RoleController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="createRoleRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [LogAction]
        [Route(Common.Url.Identity.Role.Create)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleRequest createRoleRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var command = _mapper.Map<CreateRoleCommand>(createRoleRequest);
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse());
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(CreateRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roleId">ID to update</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [LogAction]
        [Route(Common.Url.Identity.Role.Update)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> UpdateRoleAsync(Guid roleId, UpdateRoleRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var command = _mapper.Map<UpdateRoleCommand>(request);
                command.Id = roleId;
                var result = await _mediator.Send(command, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse());
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// View Role
        /// </summary>
        /// <param name="roleId">ID to view</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [LogAction]
        [Route(Common.Url.Identity.Role.View)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new ViewRoleQuery() {Id = roleId}, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ViewRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// View list of Roles
        /// </summary>
        /// <remarks>
        ///
        /// Sortable:
        ///
        ///     Name, Status
        /// </remarks>
        /// <param name="viewListRolesRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [LogAction]
        [Route(Common.Url.Identity.Role.ViewList)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewListRolesAsync([FromQuery] ViewListRolesRequest viewListRolesRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<ViewListRolesQuery>(viewListRolesRequest), cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ViewListRolesAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="roleId">ID to delete</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [LogAction]
        [Route(Common.Url.Identity.Role.Delete)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new DeleteRoleCommand() {Id = roleId}, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(DeleteRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Activate Role
        /// </summary>
        /// <param name="roleId">ID to activate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [LogAction]
        [Route(Common.Url.Identity.Role.Activate)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ActivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new ActivateRoleCommand() {Id = roleId}, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ActivateRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Deactivate Role
        /// </summary>
        /// <param name="roleId">ID to deactivate</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [LogAction]
        [Route(Common.Url.Identity.Role.Deactivate)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> DeactivateRoleAsync(Guid roleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var result = await _mediator.Send(new DeactivateRoleCommand() {Id = roleId}, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(DeactivateRoleAsync));
                Console.WriteLine(e);
                throw;
            }
        }
    }
}