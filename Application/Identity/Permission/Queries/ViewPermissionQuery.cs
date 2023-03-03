using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Permission.Responses;
using MediatR;

namespace Application.Identity.Permission.Queries
{
    [ExcludeFromCodeCoverage]
    public class ViewPermissionQuery : IRequest<Result<ViewPermissionResponse>>
    {
        public Guid PermissionId { get; set; }
    }
}