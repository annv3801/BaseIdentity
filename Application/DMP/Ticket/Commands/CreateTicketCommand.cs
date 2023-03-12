using Application.Common.Models;
using Application.DMP.Ticket.Commons;
using Application.DTO.DMP.Ticket.Requests;
using MediatR;

namespace Application.DMP.Ticket.Commands;

public class CreateTicketCommand : CreateTicketRequest, IRequest<Result<TicketResult>>
{
    
}