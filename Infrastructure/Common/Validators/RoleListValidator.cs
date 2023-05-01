using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Application.Common;
using Application.Common.Interfaces;
using Application.Identity.Role.Repositories;
using FluentValidation;

namespace Infrastructure.Common.Validators;
public static class RoleListValidator
{
    public static IRuleBuilderOptions<T, List<Guid>?> IsValidRoles<T>(this IRuleBuilder<T, List<Guid>?> ruleBuilder, IStringLocalizationService localizationService, IRoleRepository roleRepository)
    {
        return ruleBuilder
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MustBeUniqueRoleIds(localizationService)
            .MustBeExistedRoles(roleRepository, localizationService);
    }
    [ExcludeFromCodeCoverage]
    public static IRuleBuilderOptions<T, List<Guid>?> MustBeUniqueRoleIds<T>(this IRuleBuilder<T, List<Guid>?> ruleBuilder, IStringLocalizationService localizationService)
    {
        return ruleBuilder
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .Must((template, list, context) =>
            {
                if (list == null || list.Count == 0)
                    return false;
                return list.Count == list.Distinct().Count();
            }).WithMessage(localizationService[LocalizationString.Role.Duplicated].Value);
    }
    [ExcludeFromCodeCoverage]
    public static IRuleBuilderOptions<T, List<Guid>?> MustBeExistedRoles<T>(this IRuleBuilder<T, List<Guid>?> ruleBuilder, IRoleRepository roleRepository, IStringLocalizationService localizationService)
    {
        return ruleBuilder
            .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
            .MustAsync(
                async (template, list, context) =>
                {
                    //Check input list
                    if (list == null || list.Count == 0)
                        return false;
                    if (list.Count != list.Distinct().Count())
                        return false;
                    // Hit to the db 
                    var roles = await roleRepository.GetRolesAsync(list, CancellationToken.None);
                    if (roles == null || roles.Count == 0)
                        return false;
                    if (roles.Count != list.Distinct().Count())
                        return false;
                    return true;
                }).WithMessage(localizationService[LocalizationString.Role.NotFound].Value);
    }
    }
