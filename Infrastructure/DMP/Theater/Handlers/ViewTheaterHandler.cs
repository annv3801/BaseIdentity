using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Theater.Handlers;
using Application.DMP.Theater.Queries;
using Application.DMP.Theater.Services;
using Application.DTO.DMP.Theater.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Theater.Handlers;
[ExcludeFromCodeCoverage]
public class ViewTheaterHandler : IViewTheaterHandler
{
    private readonly ITheaterManagementService _theaterManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewTheaterHandler(ITheaterManagementService theaterManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _theaterManagementService = theaterManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewTheaterResponse>> Handle(ViewTheaterQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.ViewTheaterAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewTheaterResponse>.Succeed(data: result.Data);
            return Result<ViewTheaterResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewTheaterResponse>.Fail(Constants.CommitFailed);
        }
    }
}
