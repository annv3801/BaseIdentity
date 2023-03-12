using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Theater.Handlers;
using Application.DMP.Theater.Queries;
using Application.DMP.Theater.Services;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Theater.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListTheatersHandler : IViewListTheatersHandler
{
    private readonly ITheaterManagementService _theaterManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListTheatersHandler(ITheaterManagementService theaterManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _theaterManagementService = theaterManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewTheaterResponse>>> Handle(ViewListTheatersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.ViewListTheatersAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewTheaterResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewTheaterResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListTheatersHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewTheaterResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
