using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Room.Handlers;
using Application.DMP.Room.Queries;
using Application.DMP.Room.Services;
using Application.DTO.DMP.Room.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Room.Handlers;
[ExcludeFromCodeCoverage]
public class ViewRoomHandler : IViewRoomHandler
{
    private readonly IRoomManagementService _roomManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewRoomHandler(IRoomManagementService roomManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roomManagementService = roomManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewRoomResponse>> Handle(ViewRoomQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roomManagementService.ViewRoomAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewRoomResponse>.Succeed(data: result.Data);
            return Result<ViewRoomResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewRoomResponse>.Fail(Constants.CommitFailed);
        }
    }
}
