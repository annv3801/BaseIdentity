using System.Diagnostics.CodeAnalysis;
using Application.Identity.Account.Commands;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Identity.Account.Mappings.Resolvers;
[ExcludeFromCodeCoverage]
public class AccountRoleResolver : IValueResolver<CreateAccountCommand, Domain.Entities.Identity.Account, ICollection<AccountRole>?>
{
    public ICollection<AccountRole>? Resolve(CreateAccountCommand source, Domain.Entities.Identity.Account destination, ICollection<AccountRole>? destMember, ResolutionContext context)
    {
        return source.Roles?.Select(r => new AccountRole()
        {
            RoleId = r,
            AccountId = destination.Id
        }).ToList();
    }
}
