using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Theater.Commands;
using Application.DMP.Theater.Commons;
using Application.DMP.Theater.Handlers;
using Application.DMP.Theater.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Theater.Handlers;
[ExcludeFromCodeCoverage]
public class CreateTheaterHandler : ICreateTheaterHandlers
{
    private readonly ITheaterManagementService _theaterManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public CreateTheaterHandler(ITheaterManagementService theaterManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _theaterManagementService = theaterManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<TheaterResult>> Handle(CreateTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var theater = _mapper.Map<Domain.Entities.DMP.Theater>(command);
            var result = await _theaterManagementService.CreateTheaterAsync(theater, cancellationToken);
            if (result.Succeeded)
                return Result<TheaterResult>.Succeed(data: result.Data);
            return Result<TheaterResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateTheaterHandler));
            Console.WriteLine(e);
            return Result<TheaterResult>.Fail(Constants.CommitFailed);
        }
    }
}
