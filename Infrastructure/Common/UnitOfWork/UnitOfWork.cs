using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.DMP.Category.Repositories;
using Application.DMP.Film.Repositories;
using Application.DMP.FilmSchedules.Repositories;
using Application.DMP.Room.Repositories;
using Application.DMP.Seat.Repositories;
using Application.DMP.Theater.Repositories;
using Application.DMP.Ticket.Repositories;
using Application.Identity.Account.Repositories;
using Application.Identity.Permission.Repositories;
using Application.Identity.Role.Repositories;
using Domain.Entities.DMP;
using Domain.Entities.Identity;
using Infrastructure.DMP.Category.Repositories;
using Infrastructure.DMP.Film.Repositories;
using Infrastructure.DMP.FilmSchedules.Repositories;
using Infrastructure.DMP.Room.Repositories;
using Infrastructure.DMP.Seat.Repositories;
using Infrastructure.DMP.Theater.Repositories;
using Infrastructure.DMP.Ticket.Repositories;
using Infrastructure.Identity.Account.Repositories;
using Infrastructure.Identity.Permission.Repositories;
using Infrastructure.Identity.Role.Repositories;

namespace Infrastructure.Common.UnitOfWork;
[ExcludeFromCodeCoverage]
public class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UnitOfWork(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        Roles = new RoleRepository(_applicationDbContext);
        Permissions = new PermissionRepository(_applicationDbContext);
        AccountTokens = new AccountTokenRepository(_applicationDbContext);
        Accounts = new AccountRepository(_applicationDbContext);
        AccountLogins = new AccountLoginRepository(_applicationDbContext);
        Categories = new CategoryRepository(_applicationDbContext);
        Films = new FilmRepository(_applicationDbContext);
        Theaters = new TheaterRepository(_applicationDbContext);
        Rooms = new RoomRepository(_applicationDbContext);
        FilmSchedules = new FilmSchedulesRepository(_applicationDbContext);
        Seats = new SeatRepository(_applicationDbContext);
        Tickets = new TicketRepository(_applicationDbContext);
    }

    public IAccountTokenRepository AccountTokens { get; }
    public IAccountRepository Accounts { get; }
    public IRoleRepository Roles { get; }
    public IPermissionRepository Permissions { get; }
    public IAccountLoginRepository AccountLogins { get; }
    public ICategoryRepository Categories { get; }
    public IFilmRepository Films { get; }
    public ITheaterRepository Theaters { get; }
    public IRoomRepository Rooms { get; }
    public IFilmSchedulesRepository FilmSchedules { get; }
    public ITicketRepository Tickets { get; }
    public ISeatRepository Seats { get; }
    public async Task<int> CompleteAsync(CancellationToken cancellationToken)
    {
        return await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CompleteAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _applicationDbContext.SaveChangesAsync(account, cancellationToken);
    }
}
