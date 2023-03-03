using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Models;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Handlers;
using Application.Identity.Permission.Queries;
using Application.Identity.Permission.Services;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructure.Identity.Permission.Handlers
{
    [ExcludeFromCodeCoverage]
    public class ViewListPermissionsHandler : IViewListPermissionsHandler
    {
        private readonly IPermissionManagementService _permissionManagementService;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ViewListPermissionsHandler(IPermissionManagementService permissionManagementService, ILoggerService loggerService, IMapper mapper)
        {
            _permissionManagementService = permissionManagementService;
            _loggerService = loggerService;
            _mapper = mapper;
        }

        public async Task<Result<PaginationBaseResponse<ViewPermissionResponse>>> Handle(ViewListPermissionsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _permissionManagementService.ViewListPermissionsAsync(query, cancellationToken);
            }
            catch (Exception e)
            {
                _loggerService.LogCritical(e, nameof(ViewListPermissionsHandler));
                Console.WriteLine(e);
                ;
                return Result<PaginationBaseResponse<ViewPermissionResponse>>.Fail(Constants.ViewListFailed);
            }
        }
    }
}