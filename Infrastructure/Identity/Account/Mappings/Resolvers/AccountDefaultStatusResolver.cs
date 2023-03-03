using System.Diagnostics.CodeAnalysis;
using Application.Common.Configurations;
using Application.Identity.Account.Commands;
using AutoMapper;
using Domain.Enums;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Mappings.Resolvers
{
    /// <summary>
    /// If EnableRegistrationWithOtp is true, return UserStatus.PendingConfirmation else UserStatus.Active
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AccountDefaultStatusResolver : IValueResolver<CreateAccountCommand, Domain.Entities.Identity.Account, AccountStatus>
    {
        private readonly IOptions<ApplicationConfiguration> _appOptions;

        public AccountDefaultStatusResolver(IOptions<ApplicationConfiguration> appOptions)
        {
            _appOptions = appOptions;
        }

        public AccountStatus Resolve(CreateAccountCommand source, Domain.Entities.Identity.Account destination, AccountStatus destMember, ResolutionContext context)
        {
            return _appOptions.Value.EnableRegistrationWithOtp
                ? AccountStatus.Active
                : AccountStatus.Active;
        }
    }
}