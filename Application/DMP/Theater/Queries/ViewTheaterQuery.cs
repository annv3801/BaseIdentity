using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Theater.Responses;
using MediatR;

namespace Application.DMP.Theater.Queries;
[ExcludeFromCodeCoverage]
public class ViewTheaterQuery : IRequest<Result<ViewTheaterResponse>>
{
    public Guid Id { get; set; }
}
