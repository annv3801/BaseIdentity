using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Ticket.Commons;
using Application.DTO.DMP.Ticket.Requests;
using MediatR;

namespace Application.DMP.Ticket.Commands;
[ExcludeFromCodeCoverage]
public class UpdateTicketCommand : UpdateTicketRequest, IRequest<Result<TicketResult>>
{
    public Guid Id { get; set; }
}
