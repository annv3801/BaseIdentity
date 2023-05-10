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
using Application.DTO.Pagination.Responses;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Slider.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListSlidersHandler : IViewListSlidersHandler
{
    private readonly ISliderManagementService _sliderManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListSlidersHandler(ILoggerService loggerService, IMapper mapper, ISliderManagementService sliderManagementService)
    {
        _loggerService = loggerService;
        _mapper = mapper;
        _sliderManagementService = sliderManagementService;
    }

    public async Task<Result<PaginationBaseResponse<ViewSliderResponse>>> Handle(ViewListSlidersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _sliderManagementService.ViewListSliderAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewSliderResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewSliderResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListSlidersHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewSliderResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
