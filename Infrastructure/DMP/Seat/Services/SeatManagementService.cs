using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Seat.Commons;
using Application.DMP.Seat.Queries;
using Application.DMP.Seat.Repositories;
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
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISeatRepository _seatRepository;

    public SeatManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, IApplicationDbContext applicationDbContext, ISeatRepository seatRepository)
    {
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
        _seatRepository = seatRepository;
    }
    public async Task<Result<SeatResult>> CreateSeatAsync(Domain.Entities.DMP.Seat seat, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            await _seatRepository.AddAsync(seat, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
            var seat = await _seatRepository.GetSeatAsync(seatId, cancellationToken);
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
            var seat = await _seatRepository.GetSeatAsync(seatId, cancellationToken);
            if (seat.Status == DMPStatus.Deleted)
                return Result<SeatResult>.Fail(LocalizationString.Seat.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            seat.Status = DMPStatus.Deleted;
            _seatRepository.Update(seat);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
            
            var c = await _seatRepository.GetSeatAsync(seat.Id, cancellationToken);
            if (c == null)
                return Result<SeatResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = seat.Name;
            c.Status = seat.Status;
            c.ScheduleId = seat.ScheduleId;
            c.Type = seat.Type;
            _seatRepository.Update(c);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
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
            var filterQuery = await _seatRepository.ViewListSeatsAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Name != null && u.Name.ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.Name, p.ScheduleId, p.Type, p.Status, p.Id});
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
                    ScheduleId = a.ScheduleId,
                    RoomName = a.Name,
                    TheaterName = a.Name,
                    Type = a.Type,
                    Status = a.Status
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSeatsAsync));

            throw;
        }
    }

    public async Task<Result<PaginationBaseResponse<ViewSeatResponse>>> ViewListSeatsByScheduleAsync(ViewListSeatsByScheduleQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var filterQuery = await _seatRepository.ViewListSeatsByScheduleAsync(query, cancellationToken);
            var response = filterQuery.Where(x => x.ScheduleId == query.ScheduleId);
            var p1 = response.Select(p => new {p.Name, p.ScheduleId, p.Type, p.Status, p.Id});
            var schedule = _applicationDbContext.FilmSchedules;
            var q1 = p1.Join(schedule, x => x.ScheduleId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var q2 = q1.Join(room, x => x.y.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source = q2.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewSeatResponse>>.Succeed(new PaginationBaseResponse<ViewSeatResponse>()
                {
                    CurrentPage = result.CurrentPage,
                    OrderBy = result.OrderBy,
                    OrderByDesc = result.OrderByDesc,
                    PageSize = result.PageSize,
                    TotalItems = result.TotalItems,
                    TotalPages = result.TotalPages,
                    Result = {}
                });
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
                    Id = a.x.x.x.Id,
                    Name = a.x.x.x.Name,
                    ScheduleId = a.x.x.x.ScheduleId,
                    StartTime = a.x.x.y.StartTime,
                    EndTime = a.x.x.y.EndTime,
                    RoomName = a.x.y.Name,
                    TheaterName = a.y.Name,
                    Type = a.x.x.x.Type,
                    Status = a.x.x.x.Status
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
