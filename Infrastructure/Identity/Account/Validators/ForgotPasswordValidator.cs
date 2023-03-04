using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;

namespace Infrastructure.Identity.Account.Validators;
public class ForgotPasswordValidator: AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator(IStringLocalizationService localizationService )
    {
        RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).VietnamesePhoneNumber(localizationService);
    }
}
