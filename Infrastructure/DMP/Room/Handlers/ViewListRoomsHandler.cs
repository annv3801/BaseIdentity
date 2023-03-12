using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Room.Handlers;
using Application.DMP.Room.Queries;
using Application.DMP.Room.Services;
using Application.DTO.DMP.Room.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Room.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListRoomsHandler : IViewListRoomsHandler
{
    private readonly IRoomManagementService _roomManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListRoomsHandler(IRoomManagementService roomManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roomManagementService = roomManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewRoomResponse>>> Handle(ViewListRoomsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.ViewListRoomsAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewRoomResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewRoomResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListRoomsHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewRoomResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
