using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DMP.Category.Commands;
using Application.DMP.Category.Commons;
using Application.DMP.Category.Handlers;
using Application.DMP.Category.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.DMP.Category.Handlers;
[ExcludeFromCodeCoverage]
public class UpdateCategoryHandler : IUpdateCategoryHandler
{
    private readonly ICategoryManagementService _categoryManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ICategoryManagementService categoryManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _categoryManagementService = categoryManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<CategoryResult>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var category = _mapper.Map<Domain.Entities.DMP.Category>(request);
            var result = await _categoryManagementService.UpdateCategoryAsync(category, cancellationToken);
            if (result.Succeeded)
                return Result<CategoryResult>.Succeed(data: result.Data);
            return Result<CategoryResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(UpdateCategoryHandler));
            Console.WriteLine(e);
            return Result<CategoryResult>.Fail(Constants.CommitFailed);
        }
    }
}
