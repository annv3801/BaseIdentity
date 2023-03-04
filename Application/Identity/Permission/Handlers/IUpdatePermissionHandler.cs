using Application.Common.Models;
using Application.Identity.Permission.Command;
using Application.Identity.Permission.Common;
using MediatR;

namespace Application.Identity.Permission.Handlers;
public interface IUpdatePermissionHandler: IRequestHandler<UpdatePermissionCommand, Result<PermissionResult>>
{
    
}
