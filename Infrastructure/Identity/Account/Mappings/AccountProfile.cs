using Application.Common.Models.Account;
using Application.DTO.Account.Requests;
using Application.DTO.Account.Responses;
using Application.Identity.Account.Commands;
using Application.Identity.Account.Queries;
using AutoMapper;
using Domain.Enums;
using Infrastructure.Identity.Account.Mappings.Resolvers;

namespace Infrastructure.Identity.Account.Mappings;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        // Map from create command to user entity
        CreateMap<CreateAccountCommand, Domain.Entities.Identity.Account>()
            // generate new id
            .ForMember(dest => dest.Id,
                opt =>
                    opt.MapFrom(src => Guid.NewGuid()))
            // set default gender as true
            .ForMember(d => d.Gender, opt => opt.MapFrom(src =>
                src.Gender ?? true))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src =>
                src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.UserName.ToUpper()))
            
            .ForMember(d => d.Status, opt => opt.MapFrom<AccountDefaultStatusResolver>())
            .ForMember(d => d.SecurityStamp,
                opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.PasswordChangeRequired, opt => opt.MapFrom(src => false))
            // .ForMember(d => d.Otp, opt => opt.MapFrom<SmsOtpResolver>())
            // .ForMember(d => d.OtpValidEnd, opt => opt.MapFrom<SmsOtpValidEndResolver>())
            .ForMember(d => d.AccountRoles, opt => opt.MapFrom<AccountRoleResolver>())
            ;

        // Map from create request to create command
        CreateMap<CreateAccountRequest, CreateAccountCommand>();
        CreateMap<SignInWithPhoneNumberCommand, SignInWithPhoneNumberRequest>();
        CreateMap<SignInWithPhoneNumberRequest, SignInWithPhoneNumberCommand>();
        CreateMap<Domain.Entities.Identity.Account, ViewAccountResponse>()
            .ForMember(d => d.Roles, opt => opt.MapFrom<ListRoleResolver>());

        // Map last modified by and created by
        CreateMap<Domain.Entities.Identity.Account, CreatedByView>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AvatarPhoto, o => o.MapFrom(s => s.AvatarPhoto))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.MiddleName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName));
        CreateMap<Domain.Entities.Identity.Account, LastModifiedByView>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.AvatarPhoto, o => o.MapFrom(s => s.AvatarPhoto))
            .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.MiddleName))
            .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName));

        CreateMap<Domain.Entities.Identity.Account, ViewAccountResponse>()
            .ForMember(d => d.Roles, opt => opt.MapFrom<ListRoleResolver>());
        CreateMap<ViewListAccountsRequest, ViewListAccountsQuery>();
        CreateMap<ViewListAccountsQuery, ViewListAccountsRequest>();

        CreateMap<UpdateMyAccountRequest, UpdateMyAccountCommand>();
        CreateMap<UpdateMyAccountCommand, Domain.Entities.Identity.Account>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.AccountRoles, opt => opt.MapFrom<AccountRoleResolverForUpdateMyAccount>())
            ;

        // Map 2 account for update case, some field will be ignored
        CreateMap<Domain.Entities.Identity.Account, Domain.Entities.Identity.Account>()
            .BeforeMap((s, d) => { d.AccountRoles?.Clear(); })
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(d => d.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.AvatarPhoto, opt => opt.MapFrom(src => src.AvatarPhoto))
            .ForMember(d => d.CoverPhoto, opt => opt.MapFrom(src => src.CoverPhoto))
            .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(d => d.AccountRoles, opt => opt.MapFrom(src => src.AccountRoles))
            // .ForAllOtherMembers(s => s.Ignore())
            ;

        CreateMap<UpdateAccountRequest, UpdateAccountCommand>();
        CreateMap<UpdateAccountCommand, Domain.Entities.Identity.Account>()
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.AccountRoles, opt => opt.MapFrom<AccountRoleResolverForUpdate>())
            ;

        CreateMap<ActivateMyAccountCommand, ActivateMyAccountRequest>();
        CreateMap<ActivateMyAccountRequest, ActivateMyAccountCommand>();

        CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();
        CreateMap<ForgotPasswordCommand, ForgotPasswordRequest>();

        CreateMap<ChangePasswordRequest, ChangePasswordCommand>();
        CreateMap<ChangePasswordCommand, ChangePasswordRequest>();

        CreateMap<SetPasswordRequest, SetPasswordCommand>();
        CreateMap<SetPasswordCommand, SetPasswordRequest>();
        CreateMap<SetMyNewPasswordRequest, SetMyNewPasswordCommand>();
        CreateMap<SetMyNewPasswordCommand, SetMyNewPasswordRequest>();

        CreateMap<ChangePasswordAtFirstLoginRequest, ChangePasswordAtFirstLoginCommand>();
        CreateMap<ChangePasswordAtFirstLoginCommand, ChangePasswordAtFirstLoginRequest>();

        CreateMap<LinkExternalAccountLoginRequest, Domain.Entities.Identity.Account>()
            // generate new id
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            // set default gender as true
            .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.Gender ?? true))
            .ForMember(d => d.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
            .ForMember(d => d.NormalizedUserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.UserName, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom<PasswordDefaultResolver>())
            .ForMember(d => d.Status, opt => opt.MapFrom(src => AccountStatus.Active))
            .ForMember(d => d.SecurityStamp, opt => opt.MapFrom(src => Guid.NewGuid().ToString().ToUpper().Replace("-", "")))
            .ForMember(d => d.PasswordChangeRequired, opt => opt.MapFrom(src => false))
            .ForMember(d => d.AccountRoles, opt => opt.MapFrom<AccountRoleResolverForSignInOAuth>())
            .AfterMap((s, d) =>
            {
                d.PhoneNumber = null;
            })
            ;

        CreateMap<SignInWithUserNameCommand, SignInWithUserNameRequest>();
        CreateMap<SignInWithUserNameRequest, SignInWithUserNameCommand>();
    }
}
