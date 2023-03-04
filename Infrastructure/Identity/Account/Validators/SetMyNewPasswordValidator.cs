using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Validators;
public class SetMyNewPasswordValidator: AbstractValidator<SetMyNewPasswordRequest>
{
    public SetMyNewPasswordValidator(IStringLocalizationService localizationService, IOptions<PasswordOptions> passOption)
    {
        RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).VietnamesePhoneNumber(localizationService);
        RuleFor(x => x.Otp).Cascade(CascadeMode.Stop).Otp(localizationService);
        RuleFor(x => x.NewPassword).Cascade(CascadeMode.Stop)
            .Password(localizationService, passOption.Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
        RuleFor(x => x.ConfirmPassword).Cascade(CascadeMode.Stop)
            .Password(localizationService, passOption.Value)
            .Equal(x=>x.NewPassword).WithMessage(LocalizationString.PasswordValidation.FailedToConfirmPassword)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);

    }
}
