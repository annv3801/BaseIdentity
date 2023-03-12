using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Ticket.Responses;
using MediatR;

namespace Application.DMP.Ticket.Queries;
[ExcludeFromCodeCoverage]
public class ViewTicketQuery : IRequest<Result<ViewTicketResponse>>
{
    public Guid Id { get; set; }
}
