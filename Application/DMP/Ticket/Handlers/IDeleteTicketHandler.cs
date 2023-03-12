using Application.Common.Models;
using Application.DMP.Ticket.Commands;
using Application.DMP.Ticket.Commons;
using MediatR;

namespace Application.DMP.Ticket.Handlers;

public interface IDeleteTicketHandler : IRequestHandler<DeleteTicketCommand, Result<TicketResult>>
{
    
}