using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Room.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Room.Queries;
[ExcludeFromCodeCoverage]
public class ViewListRoomsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewRoomResponse>>>
{
}
