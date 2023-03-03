using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Identity.Account.Repositories;
using Domain.Enums;
using Infrastructure.Common.Repositories;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 8602
namespace Infrastructure.Identity.Account.Repositories
{
    public class AccountRepository : Repository<Domain.Entities.Identity.Account>, IAccountRepository
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AccountRepository(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Status != AccountStatus.Inactive, cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Account?> GetAccountByUserNameAsync(string userName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Status != AccountStatus.Deleted, cancellationToken);

        }

        public async Task<bool> IsDuplicatedEmailAsync(Guid accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts.AnyAsync(u => u.NormalizedEmail == email.ToUpper() && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
        }

        public async Task<bool> IsDuplicatedPhoneNumberAsync(Guid accountId, string? phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts.AnyAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper(), cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Account?> GetAccountByEmailForCheckDuplicateAsync(Guid accountId, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => (u.NormalizedEmail == email.ToUpper() || u.Email == email.ToUpper()) && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
        }

        public async Task<Domain.Entities.Identity.Account?> GetAccountByPhoneForCheckDuplicateAsync(Guid accountId, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Id != accountId && u.Status != AccountStatus.Deleted, cancellationToken);
        }

        public async Task<List<Domain.Entities.Identity.Account>> GetAccountsByRoleAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.AccountRoles.Any(ur => ur.Role.Name.ToLower().Contains(role)))
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public Task<Domain.Entities.Identity.Account> SignInByPasswordAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Identity.Account> SignInByOathAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsValidJwtAsync(Guid accountId, string jwt, string loginProvider, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await _applicationDbContext.AccountTokens.AnyAsync(t => t.AccountId == accountId && t.Token == jwt && t.LoginProvider == loginProvider, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Domain.Entities.Identity.Account?> ViewAccountDetailByAdmin(Guid userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<IQueryable<Domain.Entities.Identity.Account>> ViewListAccountsByAdminAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
            return _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .AsQueryable();
        }

        public async Task<Domain.Entities.Identity.Account?> ViewMyAccountAsync(Guid userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _applicationDbContext.Accounts
                .Include(u => u.AccountRoles)
                .ThenInclude(r => r.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public Task<Domain.Entities.Identity.Account> RegisterAccountAsync(string role, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Domain.Entities.Identity.Account>> ViewListNhaCungCapAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
            return _applicationDbContext.Accounts
                // .Include(u => u.AccountRoles)
                // .ThenInclude(r => r.Role)
                // .ThenInclude(rp => rp.RolePermissions)
                // .ThenInclude(p => p.Permission)
                .AsSplitQuery()
                .AsQueryable();
        }

        // public async Task<Result<RegisterUserDto>> RegisterUserAsync(RegisterUserDto registerUserDto, CancellationToken cancellationToken = default(CancellationToken))
        // {
        //     var user = new User()
        //     {
        //         // Email = "vudoanbt3@gmail.com",
        //         // NormalizedEmail = "VUDOANBT3@GMAIL.COM",
        //         // SecurityStamp = Guid.NewGuid().ToString()
        //         //
        //     };
        //     await base.AddAsync(user, cancellationToken);
        //     return Result<RegisterUserDto>.Succeeded();
        // }
    }
}