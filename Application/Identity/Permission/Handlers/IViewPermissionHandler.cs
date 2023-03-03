using Application.Common.Models;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Queries;
using MediatR;

namespace Application.Identity.Permission.Handlers
{
    public interface IViewPermissionHandler : IRequestHandler<ViewPermissionQuery, Result<ViewPermissionResponse>>
    {
    }
}