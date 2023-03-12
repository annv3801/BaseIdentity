using Application.Common.Interfaces;

namespace Application.DMP.Ticket.Repositories;
public interface ITicketRepository : IRepository<Domain.Entities.DMP.Ticket>
{
    Task<Domain.Entities.DMP.Ticket?> GetTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken));
    Task<IQueryable<Domain.Entities.DMP.Ticket>> ViewListTicketsAsync(CancellationToken cancellationToken = default(CancellationToken));

}
