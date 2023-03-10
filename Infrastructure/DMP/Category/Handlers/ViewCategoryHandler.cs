using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Category.Handlers;
using Application.DMP.Category.Queries;
using Application.DMP.Category.Services;
using Application.DTO.DMP.Category.Responses;
using AutoMapper;
using Domain.Interfaces;
using Infrastructure.Identity.Role.Handlers;

namespace Infrastructure.DMP.Category.Handlers;
[ExcludeFromCodeCoverage]
public class ViewCategoryHandler : IViewCategoryHandler
{
    private readonly ICategoryManagementService _categoryManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewCategoryHandler(ICategoryManagementService categoryManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _categoryManagementService = categoryManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewCategoryResponse>> Handle(ViewCategoryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.ViewCategoryAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewCategoryResponse>.Succeed(data: result.Data);
            return Result<ViewCategoryResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewCategoryResponse>.Fail(Constants.CommitFailed);
        }
    }
}
