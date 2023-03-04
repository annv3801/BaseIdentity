using System.Diagnostics.CodeAnalysis;
using Application.DTO.Account.Responses;
using Application.DTO.Role.Responses;
using AutoMapper;

namespace Infrastructure.Identity.Account.Mappings.Resolvers;
[ExcludeFromCodeCoverage]
public class ListRoleResolver : IValueResolver<Domain.Entities.Identity.Account, ViewAccountResponse, List<ViewRoleResponse>?>
{
    public List<ViewRoleResponse>? Resolve(Domain.Entities.Identity.Account source, ViewAccountResponse destination, List<ViewRoleResponse>? destMember, ResolutionContext context)
    {
        return source.AccountRoles.Select(ur => ur.Role).Select(r => new ViewRoleResponse()
        {
            Id = r.Id,
            Name = r.Name,
            NormalizedName = r.NormalizedName,
            Description = r.Description,
            Status = r.Status
        }).ToList();
    }
}
