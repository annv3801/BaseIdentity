using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Room.Responses;
using MediatR;

namespace Application.DMP.Room.Queries;
[ExcludeFromCodeCoverage]
public class ViewRoomQuery : IRequest<Result<ViewRoomResponse>>
{
    public Guid Id { get; set; }
}
