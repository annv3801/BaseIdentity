using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Validators
{
    public class SignInWithUserNameValidator : AbstractValidator<SignInWithUserNameRequest>
    {
        public SignInWithUserNameValidator(IStringLocalizationService localizationService,
            IOptions<ApplicationConfiguration> applicationConfiguration)
        {
            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage(localizationService[LocalizationString.Common.EmptyField])
                .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage(localizationService[LocalizationString.Common.EmptyField])
                .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(LocalizationString.Common.MaxLengthField);
        }
    }
}