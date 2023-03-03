using Application.Common.Models;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Queries;
using MediatR;

namespace Application.Identity.Role.Handlers
{
    public interface IViewRoleHandler: IRequestHandler<ViewRoleQuery, Result<ViewRoleResponse>>
    {
        
    }
}