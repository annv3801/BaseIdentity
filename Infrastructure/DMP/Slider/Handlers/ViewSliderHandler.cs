using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Film.Handlers;
using Application.DMP.Film.Queries;
using Application.DMP.Film.Services;
using Application.DMP.Slider.Handlers;
using Application.DMP.Slider.Queries;
using Application.DMP.Slider.Services;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.DMP.Slider.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Slider.Handlers;
[ExcludeFromCodeCoverage]
public class ViewSliderHandler : IViewSliderHandler
{
    private readonly ISliderManagementService _sliderManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewSliderHandler(ILoggerService loggerService, IMapper mapper, ISliderManagementService sliderManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _sliderManagementService = sliderManagementService;
    }

    public async Task<Result<ViewSliderResponse>> Handle(ViewSliderQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sliderManagementService.ViewSliderAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewSliderResponse>.Succeed(data: result.Data);
            return Result<ViewSliderResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateSliderHandler));
            Console.WriteLine(e);
            return Result<ViewSliderResponse>.Fail(Constants.CommitFailed);
        }
    }
}
