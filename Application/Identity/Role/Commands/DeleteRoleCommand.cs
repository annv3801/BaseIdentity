using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.Identity.Role.Commands
{
    [ExcludeFromCodeCoverage]
    public class DeleteRoleCommand : IRequest<Result<RoleResult>>
    {
        public Guid Id { get; set; }
    }
}