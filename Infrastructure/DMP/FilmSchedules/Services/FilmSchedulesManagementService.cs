using System.Linq.Dynamic.Core;
using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Film.Repositories;
using Application.DMP.FilmSchedules.Commons;
using Application.DMP.FilmSchedules.Queries;
using Application.DMP.FilmSchedules.Repositories;
using Application.DMP.FilmSchedules.Services;
using Application.DMP.Room.Repositories;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.FilmSchedules.Requests;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.FilmSchedules.Services;
public class FilmSchedulesManagementService : IFilmSchedulesManagementService
{
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IFilmRepository _filmRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IFilmSchedulesRepository _filmSchedulesRepository;

    public FilmSchedulesManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, IApplicationDbContext applicationDbContext, IFilmRepository filmRepository, IRoomRepository roomRepository, IFilmSchedulesRepository filmSchedulesRepository)
    {
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _applicationDbContext = applicationDbContext;
        _filmRepository = filmRepository;
        _roomRepository = roomRepository;
        _filmSchedulesRepository = filmSchedulesRepository;
    }
    public async Task<Result<FilmSchedulesResult>> CreateFilmSchedulesAsync(Domain.Entities.DMP.FilmSchedule filmSchedules, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var film = await _filmRepository.GetFilmAsync(filmSchedules.FilmId, cancellationToken);
            if (film == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundFilm.ToErrors(_localizationService));
            var room = await _roomRepository.GetRoomAsync(filmSchedules.RoomId, cancellationToken);
            if (room == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundRoom.ToErrors(_localizationService));
            await _filmSchedulesRepository.AddAsync(filmSchedules, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.FilmSchedules.Create,
                    Message = LocalizationString.FilmSchedules.FailedToCreate,
                    MessageParams = new object[] {filmSchedules.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
                }, cancellationToken);
                return Result<FilmSchedulesResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Create,
                Message = LocalizationString.FilmSchedules.FailedToCreate,
                MessageParams = new object[] {filmSchedules.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
            }, cancellationToken);
            return Result<FilmSchedulesResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateFilmSchedulesAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Create,
                Message = LocalizationString.FilmSchedules.FailedToCreate,
                MessageParams = new object[] {filmSchedules.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewFilmSchedulesResponse>> ViewFilmSchedulesAsync(Guid filmSchedulesId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var filmSchedules = await _filmSchedulesRepository.GetFilmSchedulesAsync(filmSchedulesId, cancellationToken);
            if (filmSchedules == null)
                return Result<ViewFilmSchedulesResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewFilmSchedulesResponse>.Succeed(data: _mapper.Map<ViewFilmSchedulesResponse>(filmSchedules));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewFilmSchedulesAsync));
            throw;
        }
    }
    public async Task<Result<FilmSchedulesResult>> DeleteFilmSchedulesAsync(Guid filmSchedulesId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find filmSchedules
            var filmSchedules = await _filmSchedulesRepository.GetFilmSchedulesAsync(filmSchedulesId, cancellationToken);
            // if (filmSchedules.Status == DMPStatus.Deleted)
                // return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.AlreadyDeleted.ToErrors(_localizationService));
            // Update data
            // filmSchedules.Status = DMPStatus.Deleted;
            _filmSchedulesRepository.Update(filmSchedules);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.FilmSchedules.Delete,
                    Message = LocalizationString.FilmSchedules.Deleted,
                    MessageParams = new object[] {filmSchedules.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
                }, cancellationToken);
                return Result<FilmSchedulesResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Delete,
                Message = LocalizationString.FilmSchedules.FailedToDelete,
                MessageParams = new object[] {filmSchedules.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
            }, cancellationToken);
            return Result<FilmSchedulesResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteFilmSchedulesAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Delete,
                Message = LocalizationString.FilmSchedules.FailedToDelete,
                MessageParams = new object[] {filmSchedulesId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<FilmSchedulesResult>> UpdateFilmSchedulesAsync(Domain.Entities.DMP.FilmSchedule filmSchedules, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var film = await _filmRepository.GetFilmAsync(filmSchedules.FilmId, cancellationToken);
            if (film == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundFilm.ToErrors(_localizationService));
            var room = await _roomRepository.GetRoomAsync(filmSchedules.RoomId, cancellationToken);
            if (room == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundRoom.ToErrors(_localizationService));
            var c = await _filmSchedulesRepository.GetFilmSchedulesAsync(filmSchedules.Id, cancellationToken);
            if (c == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.RoomId = filmSchedules.RoomId;
            c.FilmId = filmSchedules.FilmId;
            c.StartTime = filmSchedules.StartTime;
            c.EndTime = filmSchedules.EndTime;
            _filmSchedulesRepository.Update(c);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.FilmSchedules.Update,
                    Message = LocalizationString.FilmSchedules.Updated,
                    MessageParams = new object[] {filmSchedules.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
                }, cancellationToken);
                return Result<FilmSchedulesResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Update,
                Message = LocalizationString.FilmSchedules.FailedToUpdate,
                MessageParams = new object[] {filmSchedules.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(filmSchedules)
            }, cancellationToken);
            return Result<FilmSchedulesResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateFilmSchedulesAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.FilmSchedules.Update,
                Message = LocalizationString.FilmSchedules.FailedToUpdate,
                MessageParams = new object[] {filmSchedules.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>> ViewListFilmSchedulesAsync(ViewListFilmSchedulesQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            
            var filterQuery = await _filmSchedulesRepository.ViewListFilmSchedulesAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.StartTime != null && u.StartTime.ToString().ToLower().Contains(keyword)
            ).AsSplitQuery();
            var getListSchedules = p1.Select(p => new {p.RoomId, p.FilmId, p.StartTime, p.Id, p.EndTime});
            var film = _applicationDbContext.Films;
            var filmJoinSchedule = getListSchedules.Join(film, x => x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var roomJoin = filmJoinSchedule.Join(room, x => x.x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source = roomJoin.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Fail(
                    _localizationService[LocalizationString.FilmSchedules.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Succeed(new PaginationBaseResponse<ViewFilmSchedulesResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewFilmSchedulesResponse()
                {
                    Id = a.x.x.x.Id,
                    RoomId = a.x.x.x.RoomId,
                    RoomName = a.x.y.Name,
                    TheaterName = a.y.Name,
                    FilmId = a.x.x.x.FilmId,
                    FilmName = a.x.x.y.Name,
                    StartTime = a.x.x.x.StartTime,
                    EndTime = a.x.x.x.EndTime,
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulesAsync));

            throw;
        }
    }

    public async Task<Result<List<TheaterScheduleResponse>>> ViewListFilmSchedulesByTimeAsync(ViewListFilmSchedulesByTimeQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var filterQuery = await _filmSchedulesRepository.ViewListFilmSchedulesByTimeAsync(query, cancellationToken);
            var response = filterQuery.Where(p => p.StartTime.Date == query.Date.Date && p.FilmId == query.FilmId);
            var getListSchedules = response.Select(p => new {p.RoomId, p.FilmId, p.StartTime, p.Id, p.EndTime});
            var film = _applicationDbContext.Films;
            var filmJoinSchedule = getListSchedules.Join(film, x => x.FilmId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var room = _applicationDbContext.Rooms;
            var roomJoin = filmJoinSchedule.Join(room, x => x.x.RoomId, y => y.Id, (x, y) => new
            {
                x, y
            });
            var theater = _applicationDbContext.Theaters;
            var source = roomJoin.Join(theater, x => x.y.TheaterId, y => y.Id, (x, y) => new
                {
                    TheaterId = y.Id,
                    TheaterName = y.Name,
                    Schedule = new ScheduleResponse
                    {
                        Id = x.x.x.Id,
                        StartTime = x.x.x.StartTime,
                        EndTime = x.x.x.EndTime,
                        FilmId = x.x.y.Id,
                        FilmName = x.x.y.Name
                    }
                })
                .GroupBy(x => new { x.TheaterId, x.TheaterName })
                .Select(g => new TheaterScheduleResponse
                {
                    TheaterId = g.Key.TheaterId,
                    TheaterName = g.Key.TheaterName,
                    ListSchedule = g.Select(x => x.Schedule).ToList()
                });
            return Result<List<TheaterScheduleResponse>>.Succeed(source.ToList());
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulesAsync));

            throw;
        }
    }
}
