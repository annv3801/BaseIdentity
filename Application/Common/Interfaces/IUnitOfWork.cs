using Application.DMP.Category.Repositories;
using Application.DMP.Film.Repositories;
using Application.DMP.FilmSchedules.Repositories;
using Application.DMP.Room.Repositories;
using Application.DMP.Theater.Repositories;
using Application.Identity.Account.Repositories;
using Application.Identity.Permission.Repositories;
using Application.Identity.Role.Repositories;
using Domain.Entities.Identity;

namespace Application.Common.Interfaces;
public interface IUnitOfWork
{
    IAccountLoginRepository AccountLogins { get; }
    IAccountTokenRepository AccountTokens { get; }
    IAccountRepository Accounts { get; }
    IRoleRepository Roles { get; }
    IPermissionRepository Permissions { get; }
    ICategoryRepository Categories { get; }
    IFilmRepository Films { get; }
    ITheaterRepository Theaters { get; }
    IRoomRepository Rooms { get; }
    IFilmSchedulesRepository FilmSchedules { get; }
    
    Task<int> CompleteAsync(CancellationToken cancellationToken = default(CancellationToken));
    Task<int> CompleteAsync(Account account, CancellationToken cancellationToken = default(CancellationToken));
}
