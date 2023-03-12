using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Ticket.Handlers;
using Application.DMP.Ticket.Queries;
using Application.DMP.Ticket.Services;
using Application.DTO.DMP.Ticket.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Ticket.Handlers;
[ExcludeFromCodeCoverage]
public class ViewTicketHandler : IViewTicketHandler
{
    private readonly ITicketManagementService _ticketManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewTicketHandler(ITicketManagementService ticketManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _ticketManagementService = ticketManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewTicketResponse>> Handle(ViewTicketQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _ticketManagementService.ViewTicketAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewTicketResponse>.Succeed(data: result.Data);
            return Result<ViewTicketResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewTicketResponse>.Fail(Constants.CommitFailed);
        }
    }
}
