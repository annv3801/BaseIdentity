using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Seat.Handlers;
using Application.DMP.Seat.Queries;
using Application.DMP.Seat.Services;
using Application.DTO.DMP.Seat.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Seat.Handlers;
[ExcludeFromCodeCoverage]
public class ViewSeatHandler : IViewSeatHandler
{
    private readonly ISeatManagementService _seatManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewSeatHandler(ISeatManagementService seatManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _seatManagementService = seatManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewSeatResponse>> Handle(ViewSeatQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.ViewSeatAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewSeatResponse>.Succeed(data: result.Data);
            return Result<ViewSeatResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewSeatResponse>.Fail(Constants.CommitFailed);
        }
    }
}
