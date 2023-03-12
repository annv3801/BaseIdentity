using Application.Common.Interfaces;
using Application.DMP.Room.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Room.Repositories;
public class RoomRepository : Repository<Domain.Entities.DMP.Room>, IRoomRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public RoomRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.Room?> GetRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Rooms.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == roomId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Room>> ViewListRoomsAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Rooms
            .AsSplitQuery()
            .AsQueryable();
    }

}
