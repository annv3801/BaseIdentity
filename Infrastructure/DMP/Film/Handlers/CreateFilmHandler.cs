using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Film.Commands;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Handlers;
using Application.DMP.Film.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Film.Handlers;
[ExcludeFromCodeCoverage]
public class CreateFilmHandler : ICreateFilmHandlers
{
    private readonly IFilmManagementService _filmManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public CreateFilmHandler(IFilmManagementService filmManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmManagementService = filmManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<FilmResult>> Handle(CreateFilmCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var film = _mapper.Map<Domain.Entities.DMP.Films>(command);
            var result = await _filmManagementService.CreateFilmAsync(film, cancellationToken);
            if (result.Succeeded)
                return Result<FilmResult>.Succeed(data: result.Data);
            return Result<FilmResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateFilmHandler));
            Console.WriteLine(e);
            return Result<FilmResult>.Fail(Constants.CommitFailed);
        }
    }
}
