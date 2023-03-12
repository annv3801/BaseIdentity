using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Handlers;
using Application.DMP.FilmSchedules.Queries;
using Application.DMP.FilmSchedules.Services;
using Application.DTO.DMP.FilmSchedules.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.FilmSchedules.Handlers;
[ExcludeFromCodeCoverage]
public class ViewFilmSchedulesHandler : IViewFilmSchedulesHandler
{
    private readonly IFilmSchedulesManagementService _filmSchedulesManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewFilmSchedulesHandler(IFilmSchedulesManagementService filmSchedulesManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmSchedulesManagementService = filmSchedulesManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewFilmSchedulesResponse>> Handle(ViewFilmSchedulesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmSchedulesManagementService.ViewFilmSchedulesAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewFilmSchedulesResponse>.Succeed(data: result.Data);
            return Result<ViewFilmSchedulesResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewFilmSchedulesResponse>.Fail(Constants.CommitFailed);
        }
    }
}
