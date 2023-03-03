using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Account.Repositories;
using Domain.Entities.Identity;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Account.Repositories
{
    public class AccountTokenRepository : Repository<AccountToken>, IAccountTokenRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AccountTokenRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<AccountToken>> GetAccountTokensByAccountAsync(Guid accountId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.AccountTokens
                .Include(at => at.Account)
                .AsSplitQuery()
                .Where(at => at.AccountId == accountId).ToListAsync(cancellationToken);
        }

        public async Task<AccountToken> GetAccountTokensByTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.AccountTokens.FirstOrDefaultAsync(t=>t.Token==token,cancellationToken);
        }
    }
}