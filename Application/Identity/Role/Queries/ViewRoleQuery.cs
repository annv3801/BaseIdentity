using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.Role.Responses;
using MediatR;

namespace Application.Identity.Role.Queries;
[ExcludeFromCodeCoverage]
public class ViewRoleQuery : IRequest<Result<ViewRoleResponse>>
{
    public Guid Id { get; set; }
}
