using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Queries;
using Application.Identity.Role.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Role.Handlers;
[ExcludeFromCodeCoverage]
public class ViewRoleHandler : IViewRoleHandler
{
    private readonly IRoleManagementService _roleManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewRoleHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roleManagementService = roleManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<ViewRoleResponse>> Handle(ViewRoleQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roleManagementService.ViewRoleAsync(query.Id, cancellationToken);
            if (result.Succeeded)
                return Result<ViewRoleResponse>.Succeed(data: result.Data);
            return Result<ViewRoleResponse>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<ViewRoleResponse>.Fail(Constants.CommitFailed);
        }
    }
}
