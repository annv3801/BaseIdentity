using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Film.Commands;
using Application.DMP.Film.Commons;
using Application.DMP.Film.Handlers;
using Application.DMP.Film.Services;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Commons;
using Application.DMP.Slider.Handlers;
using Application.DMP.Slider.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Slider.Handlers;
[ExcludeFromCodeCoverage]
public class CreateSliderHandler : ICreateSliderHandlers
{
    private readonly ISliderManagementService _sliderManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public CreateSliderHandler(ILoggerService loggerService, IMapper mapper, ISliderManagementService sliderManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _sliderManagementService = sliderManagementService;
    }

    public async Task<Result<SliderResult>> Handle(CreateSliderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var slider = _mapper.Map<Domain.Entities.DMP.Slider>(command);
            var result = await _sliderManagementService.CreateSliderAsync(slider, cancellationToken);
            if (result.Succeeded)
                return Result<SliderResult>.Succeed(data: result.Data);
            return Result<SliderResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateSliderHandler));
            Console.WriteLine(e);
            return Result<SliderResult>.Fail(Constants.CommitFailed);
        }
    }
}
