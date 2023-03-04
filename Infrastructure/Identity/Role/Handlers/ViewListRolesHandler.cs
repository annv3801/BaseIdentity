using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Queries;
using Application.Identity.Role.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Role.Handlers;
[ExcludeFromCodeCoverage]
public class ViewListRolesHandler : IViewListRolesHandler
{
    private readonly IRoleManagementService _roleManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ViewListRolesHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roleManagementService = roleManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<PaginationBaseResponse<ViewRoleResponse>>> Handle(ViewListRolesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roleManagementService.ViewListRolesAsync(query, cancellationToken);
            if (result.Succeeded)
                return Result<PaginationBaseResponse<ViewRoleResponse>>.Succeed(data: result.Data);
            return Result<PaginationBaseResponse<ViewRoleResponse>>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(CreateRoleHandler));
            Console.WriteLine(e);
            return Result<PaginationBaseResponse<ViewRoleResponse>>.Fail(Constants.CommitFailed);
        }
    }
}
