using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.DMP.Theater.Responses;
using MediatR;

namespace Application.DMP.Seat.Queries;
[ExcludeFromCodeCoverage]
public class ViewSeatQuery : IRequest<Result<ViewSeatResponse>>
{
    public Guid Id { get; set; }
}
