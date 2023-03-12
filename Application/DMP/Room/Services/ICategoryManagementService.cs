using Application.Common.Models;
using Application.DMP.Room.Commons;
using Application.DMP.Room.Queries;
using Application.DTO.DMP.Room.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Room.Services;
public interface IRoomManagementService
{
    Task<Result<RoomResult>> CreateRoomAsync(Domain.Entities.DMP.Room room, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewRoomResponse>> ViewRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoomResult>> DeleteRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<RoomResult>> UpdateRoomAsync(Domain.Entities.DMP.Room room, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewRoomResponse>>> ViewListRoomsAsync(ViewListRoomsQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
