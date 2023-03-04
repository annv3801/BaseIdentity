using Application.Common.Models;
using Application.Identity.Role.Commands;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.Identity.Role.Handlers;
public interface IActivateRoleHandler : IRequestHandler<ActivateRoleCommand, Result<RoleResult>>
{
}
