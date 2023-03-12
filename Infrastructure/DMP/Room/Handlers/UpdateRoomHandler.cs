using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Room.Commands;
using Application.DMP.Room.Commons;
using Application.DMP.Room.Handlers;
using Application.DMP.Room.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Room.Handlers;
[ExcludeFromCodeCoverage]
public class UpdateRoomHandler : IUpdateRoomHandler
{
    private readonly IRoomManagementService _roomManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public UpdateRoomHandler(IRoomManagementService roomManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roomManagementService = roomManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<RoomResult>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var room = _mapper.Map<Domain.Entities.DMP.Room>(request);
            var result = await _roomManagementService.UpdateRoomAsync(room, cancellationToken);
            if (result.Succeeded)
                return Result<RoomResult>.Succeed(data: result.Data);
            return Result<RoomResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateRoomHandler));
            Console.WriteLine(e);
            return Result<RoomResult>.Fail(Constants.CommitFailed);
        }
    }
}
