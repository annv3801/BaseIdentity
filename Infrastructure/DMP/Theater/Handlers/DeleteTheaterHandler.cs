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
public class DeleteTheaterHandler : IDeleteTheaterHandler
{
    private readonly ITheaterManagementService _theaterManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public DeleteTheaterHandler(ITheaterManagementService theaterManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _theaterManagementService = theaterManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<TheaterResult>> Handle(DeleteTheaterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _theaterManagementService.DeleteTheaterAsync(command.Id, cancellationToken);
            if (result.Succeeded)
                return Result<TheaterResult>.Succeed(data: result.Data);
            return Result<TheaterResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteTheaterHandler));
            Console.WriteLine(e);
            return Result<TheaterResult>.Fail(Constants.CommitFailed);
        }
    }
}
