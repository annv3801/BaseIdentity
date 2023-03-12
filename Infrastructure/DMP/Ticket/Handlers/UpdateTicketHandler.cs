using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Ticket.Commands;
using Application.DMP.Ticket.Commons;
using Application.DMP.Ticket.Handlers;
using Application.DMP.Ticket.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Ticket.Handlers;
[ExcludeFromCodeCoverage]
public class UpdateTicketHandler : IUpdateTicketHandler
{
    private readonly ITicketManagementService _ticketManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public UpdateTicketHandler(ITicketManagementService ticketManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _ticketManagementService = ticketManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<TicketResult>> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var ticket = _mapper.Map<Domain.Entities.DMP.Ticket>(request);
            var result = await _ticketManagementService.UpdateTicketAsync(ticket, cancellationToken);
            if (result.Succeeded)
                return Result<TicketResult>.Succeed(data: result.Data);
            return Result<TicketResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateTicketHandler));
            Console.WriteLine(e);
            return Result<TicketResult>.Fail(Constants.CommitFailed);
        }
    }
}
