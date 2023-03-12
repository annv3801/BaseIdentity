using Application.Common.Models;
using Application.DMP.Ticket.Commons;
using MediatR;

namespace Application.DMP.Ticket.Commands;

public class DeleteTicketCommand : IRequest<Result<TicketResult>>
{
    public Guid Id { get; set; }
}