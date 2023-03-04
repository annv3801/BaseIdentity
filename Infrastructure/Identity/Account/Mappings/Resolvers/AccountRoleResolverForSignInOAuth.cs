using System.Diagnostics.CodeAnalysis;
using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using AutoMapper;
using Domain.Entities.Identity;

namespace Infrastructure.Identity.Account.Mappings.Resolvers;
[ExcludeFromCodeCoverage]
public class AccountRoleResolverForSignInOAuth : IValueResolver<LinkExternalAccountLoginRequest, Domain.Entities.Identity.Account, ICollection<AccountRole>?>
{
    private readonly IUnitOfWork _unitOfWork;

    public AccountRoleResolverForSignInOAuth(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public ICollection<AccountRole>? Resolve(LinkExternalAccountLoginRequest source, Domain.Entities.Identity.Account destination, ICollection<AccountRole>? destMember, ResolutionContext context)
    {
        return source.Roles?.Select(r => new AccountRole()
        {
            RoleId = r,
            Role = _unitOfWork.Roles.GetRoleAsync(r).Result,
            AccountId = destination.Id
        }).ToList();
    }
}
