using Application.Common.Models;
using Application.DMP.Ticket.Queries;
using Application.DTO.DMP.Ticket.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Ticket.Handlers;
public interface IViewListTicketsHandler: IRequestHandler<ViewListTicketsQuery, Result<PaginationBaseResponse<ViewTicketResponse>>>
{
    
}
