using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Seat.Commands;
using Application.DMP.Seat.Commons;
using Application.DMP.Seat.Handlers;
using Application.DMP.Seat.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Seat.Handlers;
[ExcludeFromCodeCoverage]
public class UpdateSeatHandler : IUpdateSeatHandler
{
    private readonly ISeatManagementService _seatManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public UpdateSeatHandler(ISeatManagementService seatManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _seatManagementService = seatManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<SeatResult>> Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var seat = _mapper.Map<Domain.Entities.DMP.Seat>(request);
            var result = await _seatManagementService.UpdateSeatAsync(seat, cancellationToken);
            if (result.Succeeded)
                return Result<SeatResult>.Succeed(data: result.Data);
            return Result<SeatResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateSeatHandler));
            Console.WriteLine(e);
            return Result<SeatResult>.Fail(Constants.CommitFailed);
        }
    }
}
