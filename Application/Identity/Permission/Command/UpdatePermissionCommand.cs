using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Permission.Requests;
using Application.Identity.Permission.Common;
using MediatR;

// ReSharper disable All

namespace Application.Identity.Permission.Command
{
    [ExcludeFromCodeCoverage]
    public class UpdatePermissionCommand : UpdatePermissionRequest, IRequest<Result<PermissionResult>>
    {
        public Guid Id { get; set; }
    }
}