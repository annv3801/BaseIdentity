using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Identity;

namespace Application.Identity.Account.Repositories
{
    public interface IAccountTokenRepository : IRepository<AccountToken>
    {
        Task<List<AccountToken>> GetAccountTokensByAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken));
        Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
        
    }
}