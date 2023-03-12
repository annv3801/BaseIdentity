using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Film.Handlers;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Services;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Film.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListFilmsHandler : IViewListFilmsHandler
{
    private readonly IFilmManagementService _filmManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListFilmsHandler(IFilmManagementService filmManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _filmManagementService = filmManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewFilmResponse>>> Handle(ViewListFilmsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _filmManagementService.ViewListFilmsAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewFilmResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewFilmResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListFilmsHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewFilmResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
