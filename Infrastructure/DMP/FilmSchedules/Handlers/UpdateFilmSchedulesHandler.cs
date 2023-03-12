using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Commands;
using Application.DMP.FilmSchedules.Commons;
using Application.DMP.FilmSchedules.Handlers;
using Application.DMP.FilmSchedules.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.FilmSchedules.Handlers;
[ExcludeFromCodeCoverage]
public class UpdateFilmSchedulesHandler : IUpdateFilmSchedulesHandler
{
    private readonly IFilmSchedulesManagementService _filmSchedulesManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public UpdateFilmSchedulesHandler(IFilmSchedulesManagementService filmSchedulesManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmSchedulesManagementService = filmSchedulesManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<FilmSchedulesResult>> Handle(UpdateFilmSchedulesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var filmSchedules = _mapper.Map<Domain.Entities.DMP.FilmSchedule>(request);
            var result = await _filmSchedulesManagementService.UpdateFilmSchedulesAsync(filmSchedules, cancellationToken);
            if (result.Succeeded)
                return Result<FilmSchedulesResult>.Succeed(data: result.Data);
            return Result<FilmSchedulesResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateFilmSchedulesHandler));
            Console.WriteLine(e);
            return Result<FilmSchedulesResult>.Fail(Constants.CommitFailed);
        }
    }
}
