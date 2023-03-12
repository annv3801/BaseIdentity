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
public class DeleteRoomHandler : IDeleteRoomHandler
{
    private readonly IRoomManagementService _roomManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public DeleteRoomHandler(IRoomManagementService roomManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roomManagementService = roomManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<RoomResult>> Handle(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.DeleteRoomAsync(command.Id, cancellationToken);
            if (result.Succeeded)
                return Result<RoomResult>.Succeed(data: result.Data);
            return Result<RoomResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteRoomHandler));
            Console.WriteLine(e);
            return Result<RoomResult>.Fail(Constants.CommitFailed);
        }
    }
}
