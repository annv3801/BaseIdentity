using Application.Common.Models;
using Application.DMP.Ticket.Queries;
using Application.DTO.DMP.Ticket.Responses;
using MediatR;

namespace Application.DMP.Ticket.Handlers;
public interface IViewTicketHandler: IRequestHandler<ViewTicketQuery, Result<ViewTicketResponse>>
{
    
}
