using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.Identity.Permission.Command;
using Application.Identity.Permission.Common;
using Application.Identity.Permission.Handlers;
using Application.Identity.Permission.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Permission.Handlers
{
    [ExcludeFromCodeCoverage]
    public class UpdatePermissionHandler : IUpdatePermissionHandler
    {
        private readonly IPermissionManagementService _permissionManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public UpdatePermissionHandler(IPermissionManagementService permissionManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _permissionManagementService = permissionManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<PermissionResult>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var perm = _mapper.Map<Domain.Entities.Identity.Permission>(request);
                return await _permissionManagementService.UpdatePermissionAsync(perm, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(UpdatePermissionHandler));
                Console.WriteLine(e);
                return Result<PermissionResult>.Fail(Constants.CommitFailed);
            }
        }
    }
}