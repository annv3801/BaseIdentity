﻿using Application.Common.Interfaces;
using Application.DMP.Booking.Repositories;
using Application.DMP.Booking.Services;
using Application.DMP.Category.Repositories;
using Application.DMP.Category.Services;
using Application.DMP.Film.Repositories;
using Application.DMP.Film.Services;
using Application.DMP.FilmSchedules.Repositories;
using Application.DMP.FilmSchedules.Services;
using Application.DMP.Room.Repositories;
using Application.DMP.Room.Services;
using Application.DMP.Seat.Repositories;
using Application.DMP.Seat.Services;
using Application.DMP.Theater.Repositories;
using Application.DMP.Theater.Services;
using Application.DMP.Ticket.Repositories;
using Application.DMP.Ticket.Services;
using Application.Identity.Account.Repositories;
using Application.Identity.Account.Services;
using Application.Identity.Permission.EventHandlers;
using Application.Identity.Permission.Handlers;
using Application.Identity.Permission.Repositories;
using Application.Identity.Permission.Services;
using Application.Identity.Role.EventHandlers;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Repositories;
using Application.Identity.Role.Services;
using Application.Logging.ActionLog.Services;
using Domain.Interfaces;
using Infrastructure.DMP.Booking.Repositories;
using Infrastructure.DMP.Booking.Services;
using Infrastructure.DMP.Category.Repositories;
using Infrastructure.DMP.Category.Services;
using Infrastructure.DMP.Email;
using Infrastructure.DMP.Film.Repositories;
using Infrastructure.DMP.Film.Services;
using Infrastructure.DMP.FilmSchedules.Repositories;
using Infrastructure.DMP.FilmSchedules.Services;
using Infrastructure.DMP.Room.Repositories;
using Infrastructure.DMP.Room.Services;
using Infrastructure.DMP.Seat.Repositories;
using Infrastructure.DMP.Seat.Services;
using Infrastructure.DMP.Theater.Repositories;
using Infrastructure.DMP.Theater.Services;
using Infrastructure.DMP.Ticket.Repositories;
using Infrastructure.DMP.Ticket.Services;
using Infrastructure.Identity.Account.Repositories;
using Infrastructure.Identity.Account.Services;
using Infrastructure.Identity.Permission.EventHandlers;
using Infrastructure.Identity.Permission.Handlers;
using Infrastructure.Identity.Permission.Repositories;
using Infrastructure.Identity.Permission.Services;
using Infrastructure.Identity.Role.EventHandlers;
using Infrastructure.Identity.Role.Handlers;
using Infrastructure.Identity.Role.Repositories;
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
        services.AddHttpContextAccessor();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        services.AddScoped<IAccountManagementService, AccountManagementService>();
        services.AddScoped<IAccountRepository, AccountRepository>();
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
        services.AddScoped<IAccountTokenRepository, AccountTokenRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        
        services.AddScoped<ICategoryManagementService, CategoryManagementService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IFilmManagementService, FilmManagementService>();
        services.AddScoped<IFilmRepository, FilmRepository>();
        services.AddScoped<ITheaterManagementService, TheaterManagementService>();
        services.AddScoped<ITheaterRepository, TheaterRepository>();
        services.AddScoped<IRoomManagementService, RoomManagementService>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IFilmSchedulesManagementService, FilmSchedulesManagementService>();
        services.AddScoped<IFilmSchedulesRepository, FilmSchedulesRepository>();
        services.AddScoped<ISeatManagementService, SeatManagementService>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<ITicketManagementService, TicketManagementService>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<IBookingManagementService, BookingManagementService>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
