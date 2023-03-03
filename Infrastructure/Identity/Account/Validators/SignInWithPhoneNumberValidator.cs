using Application.Common;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Account.Validators
{
    public class SignInWithPhoneNumberValidator : AbstractValidator<SignInWithPhoneNumberRequest>
    {
        public SignInWithPhoneNumberValidator(IStringLocalizationService localizationService,
            IOptions<ApplicationConfiguration> applicationConfiguration)
        {
            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).VietnamesePhoneNumber(localizationService);
            RuleFor(x => x.Password).Cascade(CascadeMode.Stop).NotEmpty()
                .WithMessage(localizationService[LocalizationString.Common.EmptyField])
                .MaximumLength(Constants.FieldLength.TextMaxLength)
                .WithMessage(LocalizationString.Common.MaxLengthField);
        }
    }
}