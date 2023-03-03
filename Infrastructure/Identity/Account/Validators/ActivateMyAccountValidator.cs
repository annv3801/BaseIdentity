using Application.Common.Interfaces;
using Application.DTO.Account.Requests;
using FluentValidation;
using Infrastructure.Common.Validators;

namespace Infrastructure.Identity.Account.Validators
{
    public class ActivateMyAccountValidator : AbstractValidator<ActivateMyAccountRequest>
    {
        public ActivateMyAccountValidator(IStringLocalizationService localizationService)
        {
            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).VietnamesePhoneNumber(localizationService);
            RuleFor(x => x.Otp).Cascade(CascadeMode.Stop).Otp(localizationService);
        }
    }
}