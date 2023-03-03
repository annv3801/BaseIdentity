using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Application.Identity.Account.Commands;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Identity.Account.Mappings.Resolvers
{
    [ExcludeFromCodeCoverage]
    public class AccountRoleResolverForUpdateMyAccount: IValueResolver<UpdateMyAccountCommand, Domain.Entities.Identity.Account, ICollection<AccountRole>?>
    {
        public ICollection<AccountRole>? Resolve(UpdateMyAccountCommand source, Domain.Entities.Identity.Account destination, ICollection<AccountRole>? destMember, ResolutionContext context)
        {
            return source.Roles?.Select(r => new AccountRole()
            {
                RoleId = r,
                AccountId = destination.Id
            }).ToList();
        }
    }
}