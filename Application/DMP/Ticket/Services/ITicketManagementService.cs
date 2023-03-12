using Application.Common.Models;
using Application.DMP.Ticket.Commons;
using Application.DMP.Ticket.Queries;
using Application.DTO.DMP.Ticket.Responses;
using Application.DTO.Pagination.Responses;

namespace Application.DMP.Ticket.Services;
public interface ITicketManagementService
{
    Task<Result<TicketResult>> CreateTicketAsync(Domain.Entities.DMP.Ticket ticket, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewTicketResponse>> ViewTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TicketResult>> DeleteTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<TicketResult>> UpdateTicketAsync(Domain.Entities.DMP.Ticket ticket, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<PaginationBaseResponse<ViewTicketResponse>>> ViewListTicketsAsync(ViewListTicketsQuery query, CancellationToken cancellationToken = default(CancellationToken));

}
