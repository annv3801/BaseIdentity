using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.DTO.Account.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Account.Handlers;
using Application.Identity.Account.Queries;
using Application.Identity.Account.Services;
using AutoMapper;

namespace Infrastructure.Identity.Account.Handlers
{
    [ExcludeFromCodeCoverage]
    public class GetListAccountsHandler : IViewListAccountsHandler
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly IMapper _mapper;

        public GetListAccountsHandler(IAccountManagementService accountManagementService, IMapper mapper)
        {
            _accountManagementService = accountManagementService;
            _mapper = mapper;
        }

        public async Task<Result<PaginationBaseResponse<ViewAccountResponse>>> Handle(ViewListAccountsQuery viewAccountQuery, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<Application.DTO.Account.Requests.ViewListAccountsRequest>(viewAccountQuery);
            return await _accountManagementService.ViewListAccountsByAdminAsync(request,cancellationToken);
        }
    }
}