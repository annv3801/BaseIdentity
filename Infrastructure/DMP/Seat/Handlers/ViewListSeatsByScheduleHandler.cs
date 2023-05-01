using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Seat.Handlers;
using Application.DMP.Seat.Queries;
using Application.DMP.Seat.Services;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Seat.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListSeatsByScheduleHandler : IViewListSeatsByScheduleHandler
{
    private readonly ISeatManagementService _seatManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListSeatsByScheduleHandler(ISeatManagementService seatManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _seatManagementService = seatManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewSeatResponse>>> Handle(ViewListSeatsByScheduleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _seatManagementService.ViewListSeatsByScheduleAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewSeatResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSeatsByScheduleHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewSeatResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
