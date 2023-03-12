using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Ticket.Handlers;
using Application.DMP.Ticket.Queries;
using Application.DMP.Ticket.Services;
using Application.DTO.DMP.Ticket.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Ticket.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListTicketsHandler : IViewListTicketsHandler
{
    private readonly ITicketManagementService _ticketManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListTicketsHandler(ITicketManagementService ticketManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _ticketManagementService = ticketManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewTicketResponse>>> Handle(ViewListTicketsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.ViewListTicketsAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewTicketResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewTicketResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListTicketsHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewTicketResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
