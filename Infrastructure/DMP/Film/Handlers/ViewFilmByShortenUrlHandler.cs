using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Film.Handlers;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Services;
using Application.DTO.DMP.Film.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Film.Handlers;
[ExcludeFromCodeCoverage]
public class ViewFilmByShortenUrlHandler : IViewFilmByShortenUrlHandler
{
    private readonly IFilmManagementService _filmManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewFilmByShortenUrlHandler(IFilmManagementService filmManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmManagementService = filmManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewFilmResponse>> Handle(ViewFilmByShortenUrlQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.ViewFilmByShortenUrlAsync(query.ShortenUrl, cancellationToken);
            if (result.Succeeded)
                return Result<ViewFilmResponse>.Succeed(data: result.Data);
            return Result<ViewFilmResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewFilmResponse>.Fail(Constants.CommitFailed);
        }
    }
}
