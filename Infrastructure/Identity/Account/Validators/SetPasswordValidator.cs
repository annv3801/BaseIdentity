using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.DTO.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Validators;
public class SetPasswordValidator : AbstractValidator<SetPasswordRequest>
{
    public SetPasswordValidator(IStringLocalizationService localizationService, IOptions<PasswordOptions> passOption)
    {
        RuleFor(x => x.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
        RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
            .Password(localizationService, passOption.Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
    }
}
