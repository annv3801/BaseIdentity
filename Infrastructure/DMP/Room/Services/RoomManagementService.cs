using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Room.Commons;
using Application.DMP.Room.Queries;
using Application.DMP.Room.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Room.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Room.Services;
public class RoomManagementService : IRoomManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public RoomManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<RoomResult>> CreateRoomAsync(Domain.Entities.DMP.Room room, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var theater = await _unitOfWork.Theaters.GetTheaterAsync(room.TheaterId, cancellationToken);
            if (theater == null)
                return Result<RoomResult>.Fail(LocalizationString.Room.NotFoundTheater.ToErrors(_localizationService));
            await _unitOfWork.Rooms.AddAsync(room, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Room.Create,
                    Message = LocalizationString.Room.FailedToCreate,
                    MessageParams = new object[] {room.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(room)
                }, cancellationToken);
                return Result<RoomResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Create,
                Message = LocalizationString.Room.FailedToCreate,
                MessageParams = new object[] {room.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(room)
            }, cancellationToken);
            return Result<RoomResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoomAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Create,
                Message = LocalizationString.Room.FailedToCreate,
                MessageParams = new object[] {room.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewRoomResponse>> ViewRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var room = await _unitOfWork.Rooms.GetRoomAsync(roomId, cancellationToken);
            if (room == null)
                return Result<ViewRoomResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewRoomResponse>.Succeed(data: _mapper.Map<ViewRoomResponse>(room));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewRoomAsync));
            throw;
        }
    }
    public async Task<Result<RoomResult>> DeleteRoomAsync(Guid roomId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find room
            var room = await _unitOfWork.Rooms.GetRoomAsync(roomId, cancellationToken);
            if (room.Status == DMPStatus.Deleted)
                return Result<RoomResult>.Fail(LocalizationString.Room.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            room.Status = DMPStatus.Deleted;
            _unitOfWork.Rooms.Update(room);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Room.Delete,
                    Message = LocalizationString.Room.Deleted,
                    MessageParams = new object[] {room.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(room)
                }, cancellationToken);
                return Result<RoomResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Delete,
                Message = LocalizationString.Room.FailedToDelete,
                MessageParams = new object[] {room.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(room)
            }, cancellationToken);
            return Result<RoomResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteRoomAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Delete,
                Message = LocalizationString.Room.FailedToDelete,
                MessageParams = new object[] {roomId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<RoomResult>> UpdateRoomAsync(Domain.Entities.DMP.Room room, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var theater = await _unitOfWork.Theaters.GetTheaterAsync(room.TheaterId, cancellationToken);
            if (theater == null)
                return Result<RoomResult>.Fail(LocalizationString.Room.NotFoundTheater.ToErrors(_localizationService));
            var c = await _unitOfWork.Rooms.GetRoomAsync(room.Id, cancellationToken);
            if (c == null)
                return Result<RoomResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            c.Name = room.Name;
            c.Status = room.Status;
            c.TheaterId = room.TheaterId;
            _unitOfWork.Rooms.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Room.Update,
                    Message = LocalizationString.Room.Updated,
                    MessageParams = new object[] {room.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(room)
                }, cancellationToken);
                return Result<RoomResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Update,
                Message = LocalizationString.Room.FailedToUpdate,
                MessageParams = new object[] {room.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(room)
            }, cancellationToken);
            return Result<RoomResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateRoomAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Room.Update,
                Message = LocalizationString.Room.FailedToUpdate,
                MessageParams = new object[] {room.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewRoomResponse>>> ViewListRoomsAsync(ViewListRoomsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _unitOfWork.Rooms.ViewListRoomsAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Name != null && u.Name.ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.Name, p.TheaterId, p.Status, p.Id});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewRoomResponse>>.Fail(
                    _localizationService[LocalizationString.Room.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewRoomResponse>>.Succeed(new PaginationBaseResponse<ViewRoomResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewRoomResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    TheaterId = a.TheaterId
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListRoomsAsync));

            throw;
        }
    }
}
