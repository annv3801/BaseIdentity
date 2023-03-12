using Application.Common.Interfaces;
using Application.DMP.Category.Services;
using Application.DMP.Film.Services;
using Application.DMP.FilmSchedules.Services;
using Application.DMP.Room.Services;
using Application.DMP.Seat.Services;
using Application.DMP.Theater.Services;
using Application.DMP.Ticket.Services;
using Application.Identity.Account.Services;
using Application.Identity.Permission.EventHandlers;
using Application.Identity.Permission.Handlers;
using Application.Identity.Permission.Services;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Services;
using Application.Logging.ActionLog.Services;
using Domain.Interfaces;
using Infrastructure.Common.UnitOfWork;
using Infrastructure.DMP.Category.Services;
using Infrastructure.DMP.Film.Services;
using Infrastructure.DMP.FilmSchedules.Services;
using Infrastructure.DMP.Room.Services;
using Infrastructure.DMP.Seat.Services;
using Infrastructure.DMP.Theater.Services;
using Infrastructure.DMP.Ticket.Services;
using Infrastructure.Identity.Account.Services;
using Infrastructure.Identity.Permission.EventHandlers;
using Infrastructure.Identity.Permission.Handlers;
using Infrastructure.Identity.Permission.Services;
using Infrastructure.Identity.Role.EventHandlers;
using Infrastructure.Identity.Role.Handlers;
using Infrastructure.Identity.Role.Services;
using Infrastructure.Logging.ActionLog.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;
public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHttpContextAccessor();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        services.AddScoped<IAccountManagementService, AccountManagementService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPermissionManagementService, PermissionManagementService>();
        services.AddScoped<IUpdatePermissionHandler, UpdatePermissionHandler>();
        services.AddScoped<IViewPermissionHandler, ViewPermissionHandler>();
        services.AddScoped<IViewListPermissionsHandler, ViewListPermissionsHandler>();
        services.AddScoped<IRoleManagementService, RoleManagementService>();
        services.AddScoped<IUpdateRoleHandler, UpdateRoleHandler>();
        services.AddScoped<ICreateRoleHandler, CreateRoleHandler>();
        services.AddScoped<IViewListRolesHandler, ViewListRolesHandler>();
        services.AddScoped<IDeleteRoleHandler, DeleteRoleHandler>();
        services.AddScoped<IActivateRoleHandler, ActivateRoleHandler>();
        services.AddScoped<IDeactivateRoleHandler, DeactivateRoleHandler>();
        services.AddScoped<IUpdatePermissionEventHandler, UpdatePermissionEventHandler>();
        services.AddScoped<IUpdateRoleEventHandler, UpdateRoleEventHandler>();
        services.AddScoped<ICreateRoleEventHandler, CreateRoleEventHandler>();
        services.AddScoped<IDeleteRoleEventHandler, DeleteRoleEventHandler>();
        services.AddScoped<IActivateRoleEventHandler, ActivateRoleEventHandler>();
        services.AddScoped<IDeactivateRoleEventHandler, DeactivateRoleEventHandler>();
        services.AddScoped<IJsonSerializerService, JsonSerializerService>();
        services.AddScoped<ILoggerService, LoggerService>();
        services.AddScoped<IActionLogService, ActionLogService>();
        services.AddScoped<IPaginationService, PaginationService>();
        services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
        services.AddScoped<IDateTime, DateTimeService>();
        
        services.AddScoped<ICategoryManagementService, CategoryManagementService>();
        services.AddScoped<IFilmManagementService, FilmManagementService>();
        services.AddScoped<ITheaterManagementService, TheaterManagementService>();
        services.AddScoped<IRoomManagementService, RoomManagementService>();
        services.AddScoped<IFilmSchedulesManagementService, FilmSchedulesManagementService>();
        services.AddScoped<ISeatManagementService, SeatManagementService>();
        services.AddScoped<ITicketManagementService, TicketManagementService>();
    }
}
