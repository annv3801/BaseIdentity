using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Seat.Commons;
using Application.DMP.Seat.Queries;
using Application.DMP.Seat.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Seat.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Seat.Services;
public class SeatManagementService : ISeatManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public SeatManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<SeatResult>> CreateSeatAsync(Domain.Entities.DMP.Seat seat, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var room = await _unitOfWork.Rooms.GetRoomAsync(seat.RoomId, cancellationToken);
            if (room == null)
                return Result<SeatResult>.Fail(LocalizationString.Seat.NotFoundRoom.ToErrors(_localizationService));
            await _unitOfWork.Seats.AddAsync(seat, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Seat.Create,
                    Message = LocalizationString.Seat.FailedToCreate,
                    MessageParams = new object[] {seat.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(seat)
                }, cancellationToken);
                return Result<SeatResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Create,
                Message = LocalizationString.Seat.FailedToCreate,
                MessageParams = new object[] {seat.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(seat)
            }, cancellationToken);
            return Result<SeatResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateSeatAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Create,
                Message = LocalizationString.Seat.FailedToCreate,
                MessageParams = new object[] {seat.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewSeatResponse>> ViewSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var seat = await _unitOfWork.Seats.GetSeatAsync(seatId, cancellationToken);
            if (seat == null)
                return Result<ViewSeatResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewSeatResponse>.Succeed(data: _mapper.Map<ViewSeatResponse>(seat));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewSeatAsync));
            throw;
        }
    }
    public async Task<Result<SeatResult>> DeleteSeatAsync(Guid seatId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find seat
            var seat = await _unitOfWork.Seats.GetSeatAsync(seatId, cancellationToken);
            if (seat.Status == DMPStatus.Deleted)
                return Result<SeatResult>.Fail(LocalizationString.Seat.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            seat.Status = DMPStatus.Deleted;
            _unitOfWork.Seats.Update(seat);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Seat.Delete,
                    Message = LocalizationString.Seat.Deleted,
                    MessageParams = new object[] {seat.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(seat)
                }, cancellationToken);
                return Result<SeatResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Delete,
                Message = LocalizationString.Seat.FailedToDelete,
                MessageParams = new object[] {seat.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(seat)
            }, cancellationToken);
            return Result<SeatResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteSeatAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Delete,
                Message = LocalizationString.Seat.FailedToDelete,
                MessageParams = new object[] {seatId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<SeatResult>> UpdateSeatAsync(Domain.Entities.DMP.Seat seat, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var room = await _unitOfWork.Rooms.GetRoomAsync(seat.RoomId, cancellationToken);
            if (room == null)
                return Result<SeatResult>.Fail(LocalizationString.Seat.NotFoundRoom.ToErrors(_localizationService));
            var c = await _unitOfWork.Seats.GetSeatAsync(seat.Id, cancellationToken);
            if (c == null)
                return Result<SeatResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = seat.Name;
            c.Status = seat.Status;
            c.RoomId = seat.RoomId;
            c.Type = seat.Type;
            _unitOfWork.Seats.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Seat.Update,
                    Message = LocalizationString.Seat.Updated,
                    MessageParams = new object[] {seat.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(seat)
                }, cancellationToken);
                return Result<SeatResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Update,
                Message = LocalizationString.Seat.FailedToUpdate,
                MessageParams = new object[] {seat.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(seat)
            }, cancellationToken);
            return Result<SeatResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateSeatAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Seat.Update,
                Message = LocalizationString.Seat.FailedToUpdate,
                MessageParams = new object[] {seat.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsAsync(ViewListSeatsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _unitOfWork.Seats.ViewListSeatsAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Name != null && u.Name.ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.Name, p.RoomId, p.Type, p.Status, p.Id});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSeatResponse>>.Fail(
                    _localizationService[LocalizationString.Seat.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(new PaginationBaseResponse<ViewSeatResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewSeatResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    RoomId = a.RoomId,
                    Type = a.Type
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSeatsAsync));

            throw;
        }
    }
}
