using Application.Common;
using Application.Common.Interfaces;
using Domain.Extensions;
using FluentValidation;

namespace Infrastructure.Identity.Account.Validators;
public class CreateAccountEntityValidator : AbstractValidator<Domain.Entities.Identity.Account>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountEntityValidator(IStringLocalizationService localizationService, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        // Phone number must be unique
        RuleFor(u => u.PhoneNumber).MustAsync(IsUniquePhoneNumber)
            .WithMessage(localizationService[LocalizationString.Common.DuplicatedField].Value);
    }

    private async Task<bool> IsUniquePhoneNumber(string? phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (phoneNumber.IsMissing()) return true;
        var existedAccount = await _unitOfWork.Accounts.GetAccountByPhoneNumberAsync(phoneNumber, cancellationToken);
        return existedAccount == null;
    }
}
