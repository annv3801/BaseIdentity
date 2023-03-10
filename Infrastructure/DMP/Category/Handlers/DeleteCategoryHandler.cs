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
public class DeleteCategoryHandler : IDeleteCategoryHandler
{
    private readonly ICategoryManagementService _categoryManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public DeleteCategoryHandler(ICategoryManagementService categoryManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _categoryManagementService = categoryManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<CategoryResult>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryManagementService.DeleteCategoryAsync(command.Id, cancellationToken);
            if (result.Succeeded)
                return Result<CategoryResult>.Succeed(data: result.Data);
            return Result<CategoryResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(DeleteCategoryHandler));
            Console.WriteLine(e);
            return Result<CategoryResult>.Fail(Constants.CommitFailed);
        }
    }
}
