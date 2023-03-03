using System.Linq;
using Application.Common;
using Application.Common.Interfaces;
using Application.DTO.Role.Requests;
using FluentValidation;

namespace Infrastructure.Identity.Role.Validators
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator(IStringLocalizationService localizationService, IUnitOfWork unitOfWork)
        {
            RuleFor(d => d.Name).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
                .MaximumLength(Constants.FieldLength.TextMaxLength).WithMessage(localizationService[LocalizationString.Common.MaxLengthField].Value);
            RuleFor(d => d.Description).Cascade(CascadeMode.Stop)
                .MaximumLength(Constants.FieldLength.DescriptionMaxLength).WithMessage(localizationService[LocalizationString.Common.MaxLengthField].Value)
                .When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(d => d.Status).IsInEnum().WithMessage(localizationService[LocalizationString.Common.NotValidEnumValue].Value);
            RuleFor(d => d.Permissions).Must(d => d.Count > 0).WithMessage(localizationService[LocalizationString.Role.PermissionsRequired].Value)
                .Must(d => d.Count == d.Distinct().Count()).WithMessage(localizationService[LocalizationString.Permission.Duplicated].Value)
                .MustAsync(async (x, cancellationToken) =>
                {
                    var uniquePermIds = x.Distinct().ToList();
                    return await unitOfWork.Permissions.AllAsync(p => uniquePermIds.Contains(p.Id), cancellationToken);
                }).WithMessage(localizationService[LocalizationString.Permission.NotFound].Value)
                ;
        }
    }
}