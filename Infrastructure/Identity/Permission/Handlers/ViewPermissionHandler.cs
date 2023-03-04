using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Handlers;
using Application.Identity.Permission.Queries;
using Application.Identity.Permission.Services;
using Domain.Interfaces;

namespace Infrastructure.Identity.Permission.Handlers;
[ExcludeFromCodeCoverage]
public class ViewPermissionHandler : IViewPermissionHandler
{
    private readonly ILoggerService _loggerService;
    private readonly IPermissionManagementService _permissionManagementService;

    public ViewPermissionHandler(ILoggerService loggerService, IPermissionManagementService permissionManagementService)
    {
        _loggerService = loggerService;
        _permissionManagementService = permissionManagementService;
    }

    public async Task<Result<ViewPermissionResponse>> Handle(ViewPermissionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _permissionManagementService.ViewPermissionAsync(request.PermissionId, cancellationToken);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ViewPermissionHandler));
            Console.WriteLine(e);
            return Result<ViewPermissionResponse>.Fail(Constants.CommitFailed);
        }
    }
}
