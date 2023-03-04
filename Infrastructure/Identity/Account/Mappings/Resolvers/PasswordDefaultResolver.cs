using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using AutoMapper;

namespace Infrastructure.Identity.Account.Mappings.Resolvers;
public class PasswordDefaultResolver: IValueResolver<LinkExternalAccountLoginRequest, Domain.Entities.Identity.Account, string>
{
    private readonly IPasswordGeneratorService _passwordGenerator;

    public PasswordDefaultResolver(IPasswordGeneratorService passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
    }

    public string Resolve(LinkExternalAccountLoginRequest source, Domain.Entities.Identity.Account destination, string destMember, ResolutionContext context)
    {
        return _passwordGenerator.HashPassword(_passwordGenerator.GenerateRandomPassword());
    }
}
