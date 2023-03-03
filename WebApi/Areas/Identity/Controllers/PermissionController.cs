using Application.Common;
using Application.DTO.Permission.Requests;
using Application.Identity.Permission.Command;
using Application.Identity.Permission.Queries;
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
    [SwaggerTag(Constants.SwaggerTags.Permission)]
    public class PermissionController : ControllerBase
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
        public PermissionController(IMediator mediator, IMapper mapper, ILoggerService loggerService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        /// <summary>
        /// To update Permission
        /// </summary>
        /// <param name="permId">Permission Id</param>
        /// <param name="updatePermissionRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [LogAction]
        [Route(Common.Url.Identity.Permission.Update)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> UpdatePermissionAsync(Guid permId, UpdatePermissionRequest updatePermissionRequest, CancellationToken cancellationToken)
        {
            try
            {
                var updatePermissionCommand = new UpdatePermissionCommand()
                {
                    Id = permId,
                    Description = updatePermissionRequest.Description,
                    Name = updatePermissionRequest.Name
                };
                var result = await _mediator.Send(updatePermissionCommand, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse());
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdatePermissionAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// To view Permission
        /// </summary>
        /// <param name="permId">Permission Id</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [LogAction]
        [Route(Common.Url.Identity.Permission.View)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewPermissionAsync(Guid permId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(new ViewPermissionQuery() {PermissionId = permId}, cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ViewPermissionAsync));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// To view list Permissions
        /// </summary>
        /// <remarks>
        ///
        /// Sortable:
        ///
        ///     Name, Code
        /// </remarks>
        /// <param name="viewListPermissionsRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [LogAction]
        [Route(Common.Url.Identity.Permission.ViewList)]
        [SwaggerResponse(StatusCodes.Status200OK, LocalizationString.Common.Success, typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status202Accepted, LocalizationString.Common.Error, typeof(FailureResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, LocalizationString.Common.DataValidationError, typeof(InvalidModelStateResponse))]
        [Produces(Constants.MimeTypes.Application.Json)]
        public async Task<IActionResult> ViewListPermissionsAsync([FromQuery] ViewListPermissionsRequest viewListPermissionsRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(_mapper.Map<ViewListPermissionsQuery>(viewListPermissionsRequest), cancellationToken);
                if (result.Succeeded)
                    return Ok(new SuccessResponse(data: result.Data));
                return Accepted(new FailureResponse(result.Errors));    
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ViewListPermissionsAsync));
                Console.WriteLine(e);
                throw;
            }
        }
    }
}