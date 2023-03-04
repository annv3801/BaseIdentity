using System.Diagnostics.CodeAnalysis;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Role.Commands;
using Application.Identity.Role.Commons;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Role.Handlers;
[ExcludeFromCodeCoverage]
public class ActivateRoleHandler : IActivateRoleHandler
{
    private readonly IRoleManagementService _roleManagementService;
    private readonly ILoggerService _loggerService;
    private readonly IMapper _mapper;

    public ActivateRoleHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
    {
        _roleManagementService = roleManagementService;
        _loggerService = loggerService;
        _mapper = mapper;
    }

    public async Task<Result<RoleResult>> Handle(ActivateRoleCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _roleManagementService.ActivateRoleAsync(command.Id, cancellationToken);
            if (result.Succeeded)
                return Result<RoleResult>.Succeed(data: result.Data);
            return Result<RoleResult>.Fail(result.Errors);
        }
        catch (Exception e)
        {
            _loggerService.LogCritical(e, nameof(ActivateRoleHandler));
            Console.WriteLine(e);
            return Result<RoleResult>.Fail(Constants.CommitFailed);
        }
    }
}
