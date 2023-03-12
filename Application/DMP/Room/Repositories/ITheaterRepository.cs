using Application.Common.Interfaces;

namespace Application.DMP.Room.Repositories;
public interface IRoomRepository : IRepository<Domain.Entities.DMP.Room>
{
    Task<Domain.Entities.DMP.Room?> GetRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Room>> ViewListRoomsAsync(CancellationToken cancellationToken = default(CancellationToken));

}
