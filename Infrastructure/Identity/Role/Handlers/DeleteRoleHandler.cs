using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Role.Commands;
using Application.Identity.Role.Commons;
using Application.Identity.Role.Handlers;
using Application.Identity.Role.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Role.Handlers
{
    [ExcludeFromCodeCoverage]
    public class DeleteRoleHandler : IDeleteRoleHandler
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public DeleteRoleHandler(IRoleManagementService roleManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _roleManagementService = roleManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<RoleResult>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
              
                var result = await _roleManagementService.DeleteRoleAsync(command.Id, cancellationToken);
                if (result.Succeeded)
                    return Result<RoleResult>.Succeed(data: result.Data);
                return Result<RoleResult>.Fail(result.Errors);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdateRoleHandler));
                Console.WriteLine(e);
                return Result<RoleResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}