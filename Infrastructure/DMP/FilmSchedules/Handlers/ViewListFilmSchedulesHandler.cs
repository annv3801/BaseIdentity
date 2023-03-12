using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.FilmSchedules.Handlers;
using Application.DMP.FilmSchedules.Queries;
using Application.DMP.FilmSchedules.Services;
using Application.DTO.DMP.FilmSchedules.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.FilmSchedules.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListFilmSchedulesHandler : IViewListFilmSchedulesHandler
{
    private readonly IFilmSchedulesManagementService _filmSchedulesManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListFilmSchedulesHandler(IFilmSchedulesManagementService filmSchedulesManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmSchedulesManagementService = filmSchedulesManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>> Handle(ViewListFilmSchedulesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmSchedulesManagementService.ViewListFilmSchedulesAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmSchedulesHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewFilmSchedulesResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
