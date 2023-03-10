using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DTO.Role.Requests;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.Identity.Role.Commands;
[ExcludeFromCodeCoverage]
public class CreateRoleCommand : CreateRoleRequest, IRequest<Result<RoleResult>>
{
}
