using Application.Common.Interfaces;
using Application.DMP.Ticket.Repositories;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Ticket.Repositories;
public class TicketRepository : Repository<Domain.Entities.DMP.Ticket>, ITicketRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public TicketRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Domain.Entities.DMP.Ticket?> GetTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken))
    { 
        return await _applicationDbContext.Tickets.AsSplitQuery().FirstOrDefaultAsync(r => r.Id == ticketId, cancellationToken);
    }

    public async Task<IQueryable<Domain.Entities.DMP.Ticket>> ViewListTicketsAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        await Task.CompletedTask;
        return _applicationDbContext.Tickets
            .AsSplitQuery()
            .AsQueryable();
    }

}
