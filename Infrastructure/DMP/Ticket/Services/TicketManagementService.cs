using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Repositories;
using Application.DMP.Ticket.Commons;
using Application.DMP.Ticket.Queries;
using Application.DMP.Ticket.Repositories;
using Application.DMP.Ticket.Services;
using Application.DTO.ActionLog.Requests;
using Application.DTO.DMP.Ticket.Responses;
using Application.DTO.Pagination.Responses;
using Application.Logging.ActionLog.Services;
using AutoMapper;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DMP.Ticket.Services;
public class TicketManagementService : ITicketManagementService
{
    private readonly ILoggerService _loggerService;
    private readonly IActionLogService _actionLogService;
    private readonly IStringLocalizationService _localizationService;
    private readonly IJsonSerializerService _jsonSerializerService;
    private readonly IMapper _mapper;
    private readonly IPaginationService _paginationService;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ITicketRepository _ticketRepository;
    private readonly IFilmSchedulesRepository _filmSchedulesRepository;

    public TicketManagementService(ILoggerService loggerService, IActionLogService actionLogService, IStringLocalizationService localizationService, IJsonSerializerService jsonSerializerService, IMapper mapper, IPaginationService paginationService, ITicketRepository ticketRepository, IApplicationDbContext applicationDbContext, IFilmSchedulesRepository filmSchedulesRepository)
    {
        _loggerService = loggerService;
        _actionLogService = actionLogService;
        _localizationService = localizationService;
        _jsonSerializerService = jsonSerializerService;
        _mapper = mapper;
        _paginationService = paginationService;
        _ticketRepository = ticketRepository;
        _applicationDbContext = applicationDbContext;
        _filmSchedulesRepository = filmSchedulesRepository;
    }
    public async Task<Result<TicketResult>> CreateTicketAsync(Domain.Entities.DMP.Ticket ticket, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var schedule = await _filmSchedulesRepository.GetFilmSchedulesAsync(ticket.ScheduleId, cancellationToken);
            if (schedule == null)
                return Result<TicketResult>.Fail(LocalizationString.Ticket.NotFoundSchedule.ToErrors(_localizationService));
            await _ticketRepository.AddAsync(ticket, cancellationToken);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Ticket.Create,
                    Message = LocalizationString.Ticket.FailedToCreate,
                    MessageParams = new object[] {ticket.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(ticket)
                }, cancellationToken);
                return Result<TicketResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Create,
                Message = LocalizationString.Ticket.FailedToCreate,
                MessageParams = new object[] {ticket.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(ticket)
            }, cancellationToken);
            return Result<TicketResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateTicketAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Create,
                Message = LocalizationString.Ticket.FailedToCreate,
                MessageParams = new object[] {ticket.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<ViewTicketResponse>> ViewTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find role
            var ticket = await _ticketRepository.GetTicketAsync(ticketId, cancellationToken);
            if (ticket == null)
                return Result<ViewTicketResponse>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            return Result<ViewTicketResponse>.Succeed(data: _mapper.Map<ViewTicketResponse>(ticket));
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewTicketAsync));
            throw;
        }
    }
    public async Task<Result<TicketResult>> DeleteTicketAsync(Guid ticketId, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            // Find ticket
            var ticket = await _ticketRepository.GetTicketAsync(ticketId, cancellationToken);
            // if (ticket.Status == DMPStatus.Deleted)
            //     return Result<TicketResult>.Fail(LocalizationString.Ticket.AlreadyDeleted.ToErrors(_localizationService));
            // // Update data
            // ticket.Status = DMPStatus.Deleted;
            _ticketRepository.Update(ticket);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Ticket.Delete,
                    Message = LocalizationString.Ticket.Deleted,
                    MessageParams = new object[] {ticket.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(ticket)
                }, cancellationToken);
                return Result<TicketResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Delete,
                Message = LocalizationString.Ticket.FailedToDelete,
                MessageParams = new object[] {ticket.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(ticket)
            }, cancellationToken);
            return Result<TicketResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteTicketAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Delete,
                Message = LocalizationString.Ticket.FailedToDelete,
                MessageParams = new object[] {ticketId.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<TicketResult>> UpdateTicketAsync(Domain.Entities.DMP.Ticket ticket, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var schedule = await _filmSchedulesRepository.GetFilmSchedulesAsync(ticket.ScheduleId, cancellationToken);
            if (schedule == null)
                return Result<TicketResult>.Fail(LocalizationString.Ticket.NotFoundSchedule.ToErrors(_localizationService));
            var c = await _ticketRepository.GetTicketAsync(ticket.Id, cancellationToken);
            if (c == null)
                return Result<TicketResult>.Fail(LocalizationString.Common.ItemNotFound.ToErrors(_localizationService));
            // Update data
            c.Name = ticket.Name;
            c.ScheduleId = ticket.ScheduleId;
            c.Price = ticket.Price;
            c.Type = ticket.Type;
            _ticketRepository.Update(c);
            var result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                await _actionLogService.LogSucceededEventAsync(new CreateActionLogRequest()
                {
                    Action = Constants.Actions.DMP.Ticket.Update,
                    Message = LocalizationString.Ticket.Updated,
                    MessageParams = new object[] {ticket.Id.ToString()},
                    ExtraInfo = _jsonSerializerService.Serialize(ticket)
                }, cancellationToken);
                return Result<TicketResult>.Succeed();
            }

            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Update,
                Message = LocalizationString.Ticket.FailedToUpdate,
                MessageParams = new object[] {ticket.Id.ToString()},
                ExtraInfo = _jsonSerializerService.Serialize(ticket)
            }, cancellationToken);
            return Result<TicketResult>.Fail(Constants.CommitFailed);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateTicketAsync));
            await _actionLogService.LogFailedEventAsync(new CreateActionLogRequest()
            {
                Action = Constants.Actions.DMP.Ticket.Update,
                Message = LocalizationString.Ticket.FailedToUpdate,
                MessageParams = new object[] {ticket.Id.ToString()},
                ExtraInfo = e.ToString()
            }, cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Result<PaginationBaseResponse<ViewTicketResponse>>> ViewListTicketsAsync(ViewListTicketsQuery query, CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            var keyword = query.Keyword?.ToLower() ?? string.Empty;
            var filterQuery = await _ticketRepository.ViewListTicketsAsync(cancellationToken);
            var p1 = filterQuery.Where(
                u => keyword.Length <= 0
                     || u.Price != null && u.Price.ToString().ToLower().Contains(keyword)
            ).AsSplitQuery();
            var source = p1.Select(p => new {p.ScheduleId, p.Price, p.Type, p.Name, p.Id});
            var result = await _paginationService.PaginateAsync(source, query.Page, query.OrderBy, query.OrderByDesc, query.Size, cancellationToken);
            if (result.Result.Count == 0)
            {
                return Result<PaginationBaseResponse<ViewTicketResponse>>.Fail(
                    _localizationService[LocalizationString.Ticket.FailedToViewList].Value.ToErrors(_localizationService));
            }
            return Result<PaginationBaseResponse<ViewTicketResponse>>.Succeed(new PaginationBaseResponse<ViewTicketResponse>()
            {
                CurrentPage = result.CurrentPage,
                OrderBy = result.OrderBy,
                OrderByDesc = result.OrderByDesc,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages,
                Result = result.Result.Select(a => new ViewTicketResponse()
                {
                    Id = a.Id,
                    Name = a.Name,
                    ScheduleId = a.ScheduleId,
                    Price = a.Price,
                    Type = a.Type
                }).ToList()
            });
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListTicketsAsync));

            throw;
        }
    }
}
