using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Film.Services;
public class FilmManagementService : IFilmManagementService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;

    public FilmManagementService(IUnitOfWork unitOfWork, ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService)
    {
        _unitOfWork = unitOfWork;
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
    }
    public async Task<Result<FilmResult>> CreateFilmAsync(Domain.Entities.DMP.Films film, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var category = await _unitOfWork.Categories.GetCategoryAsync(film.CategoryId, cancellationToken);
            if (category == null)
                return Result<FilmResult>.Fail(LocalizationString.Film.NotFoundCategory.ToErrors(_localizationService));
            await _unitOfWork.Films.AddAsync(film, cancellationToken);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Film.Create,
                    Message = LocalizationString.Film.FailedToCreate,
                    MessageParams = new object[] {film.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(film)
                }, cancellationToken);
                return Result<FilmResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {film.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(film)
            }, cancellationToken);
            return Result<FilmResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateFilmAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Create,
                Message = LocalizationString.Film.FailedToCreate,
                MessageParams = new object[] {film.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewFilmResponse>> ViewFilmAsync(Guid filmId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var film = await _unitOfWork.Films.GetFilmAsync(filmId, cancellationToken);
            if (film == null)
                return Result<ViewFilmResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewFilmResponse>.Succeed(new ViewFilmResponse()
            {
                Id = film.Id,
                Name = film.Name,
                ShortenUrl = film.ShortenUrl,
                Description = film.Description,
                CategoryId = film.CategoryId,
                Director = film.Director,
                Genre = film.Genre,
                Actor = film.Actor,
                Premiere = film.Premiere,
                Duration = film.Duration,
                Language = film.Language,
                Rated = film.Rated
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewFilmAsync));
            throw;
        }
    }
    public async Task<Result<FilmResult>> UpdateFilmAsync(Domain.Entities.DMP.Films film, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var category = await _unitOfWork.Categories.GetCategoryAsync(film.CategoryId, cancellationToken);
            if (category == null)
                return Result<FilmResult>.Fail(LocalizationString.Film.NotFoundCategory.ToErrors(_localizationService));
            var c = await _unitOfWork.Films.GetFilmAsync(film.Id, cancellationToken);
            if (c == null)
                    return Result<FilmResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = film.Name;
            c.ShortenUrl = film.ShortenUrl;
            _unitOfWork.Films.Update(c);
            var result = await _unitOfWork.CompleteAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Film.Update,
                    Message = LocalizationString.Film.Updated,
                    MessageParams = new object[] {film.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(film)
                }, cancellationToken);
                return Result<FilmResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Update,
                Message = LocalizationString.Film.FailedToUpdate,
                MessageParams = new object[] {film.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(film)
            }, cancellationToken);
            return Result<FilmResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateFilmAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Film.Update,
                Message = LocalizationString.Film.FailedToUpdate,
                MessageParams = new object[] {film.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewFilmResponse>>> ViewListFilmsAsync(ViewListFilmsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _unitOfWork.Films.ViewListFilmsAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Name != null && u.Name.ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.Name, p.ShortenUrl, p.Id, p.CategoryId, p.Actor, p.Director, p.Duration, p.Genre, p.Language, p.Premiere, p.Rated, p.Description});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewFilmResponse>>.Fail(
                    _localizationService[LocalizationString.Film.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewFilmResponse>>.Succeed(new PaginationBaseResponse<ViewFilmResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewFilmResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ShortenUrl = a.ShortenUrl,
                    Description = a.Description,
                    CategoryId = a.CategoryId,
                    Director = a.Director,
                    Genre = a.Genre,
                    Actor = a.Actor,
                    Premiere = a.Premiere,
                    Duration = a.Duration,
                    Language = a.Language,
                    Rated = a.Rated
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmsAsync));

            throw;
        }
    }
}
