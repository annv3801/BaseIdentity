using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Category.Handlers;
using Application.DMP.Category.Queries;
using Application.DMP.Category.Services;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Queries;
using Application.Identity.Role.Services;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Category.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListCategoriesHandler : IViewListCategoriesHandler
{
    private readonly ICategoryManagementService _categoryManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListCategoriesHandler(ICategoryManagementService categoryManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _categoryManagementService = categoryManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewCategoryResponse>>> Handle(ViewListCategoriesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.ViewListCategoriesAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewCategoryResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewCategoryResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewListCategoriesHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewCategoryResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
