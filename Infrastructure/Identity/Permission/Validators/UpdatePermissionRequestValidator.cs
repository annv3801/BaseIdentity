using Application.Common;
using Application.Common.Interfaces;
using Application.DTO.Permission.Requests;
using FluentValidation;

namespace Infrastructure.Identity.Permission.Validators;
public class UpdatePermissionRequestValidator : AbstractValidator<UpdatePermissionRequest>
{
    public UpdatePermissionRequestValidator(IStringLocalizationService localizationService)
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop).NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(localizationService[LocalizationString.Common.MaxLengthField].Value);
        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop).MaximumLength(Constants.FieldLength.DescriptionMaxLength).WithMessage(localizationService[LocalizationString.Common.MaxLengthField].Value).When(x => !string.IsNullOrEmpty(x.Description));
    }
}
