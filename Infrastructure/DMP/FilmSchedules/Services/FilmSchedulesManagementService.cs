using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Commons;
using Application.DMP.FilmSchedules.Queries;
using Application.DMP.FilmSchedules.Services;
using Application.DTO.ActionLog.Requests;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public FilmSchedulesManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<FilmSchedulesResult>> CreateFilmSchedulesAsync(Domain.Entities.DMP.FilmSchedule filmSchedules, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var film = await _unitOfWork.Films.GetFilmAsync(filmSchedules.FilmId, cancellationToken);
            if (film == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundFilm.ToErrors(_localizationService));
            var room = await _unitOfWork.Rooms.GetRoomAsync(filmSchedules.RoomId, cancellationToken);
            if (room == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundRoom.ToErrors(_localizationService));
            await _unitOfWork.FilmSchedules.AddAsync(filmSchedules, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
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
            var filmSchedules = await _unitOfWork.FilmSchedules.GetFilmSchedulesAsync(filmSchedulesId, cancellationToken);
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
            var filmSchedules = await _unitOfWork.FilmSchedules.GetFilmSchedulesAsync(filmSchedulesId, cancellationToken);
            // if (filmSchedules.Status == DMPStatus.Deleted)
            //     return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.AlreadyDeleted.ToErrors(_localizationService));
            // // Update data
            // filmSchedules.Status = DMPStatus.Deleted;
            _unitOfWork.FilmSchedules.Update(filmSchedules);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
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
            var film = await _unitOfWork.Films.GetFilmAsync(filmSchedules.FilmId, cancellationToken);
            if (film == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundFilm.ToErrors(_localizationService));
            var room = await _unitOfWork.Rooms.GetRoomAsync(filmSchedules.RoomId, cancellationToken);
            if (room == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.FilmSchedules.NotFoundRoom.ToErrors(_localizationService));
            var c = await _unitOfWork.FilmSchedules.GetFilmSchedulesAsync(filmSchedules.Id, cancellationToken);
            if (c == null)
                return Result<FilmSchedulesResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.RoomId = filmSchedules.RoomId;
            c.FilmId = filmSchedules.FilmId;
            c.StartTime = filmSchedules.StartTime;
            c.EndTime = filmSchedules.EndTime;
            _unitOfWork.FilmSchedules.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
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
            var filterQuery = await _unitOfWork.FilmSchedules.ViewListFilmSchedulesAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.StartTime != null && u.StartTime.ToString().ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.RoomId, p.FilmId, p.StartTime, p.Id, p.EndTime});
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
                    Id = a.Id,
                    RoomId = a.RoomId,
                    FilmId = a.FilmId,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulesAsync));

            throw;
        }
    }
}
