using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Ticket.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Ticket.Queries;
[ExcludeFromCodeCoverage]
public class ViewListTicketsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewTicketResponse>>>
{
}
