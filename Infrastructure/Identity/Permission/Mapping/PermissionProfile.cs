using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Requests;
using Application.DTO.Permission.Responses;
using Application.Identity.Permission.Command;
using Application.Identity.Permission.Queries;
using AutoMapper;

namespace Infrastructure.Identity.Permission.Mapping;
public class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<UpdatePermissionCommand, Domain.Entities.Identity.Permission>()
            .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.NormalizedName, o => o.MapFrom(s => s.Name.ToUpper()));

        CreateMap<Domain.Entities.Identity.Permission, ViewPermissionResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.CreatedBy, o => o.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.LastModifiedBy, o => o.MapFrom(s => s.LastModifiedBy));

        CreateMap<PaginationBaseResponse<Domain.Entities.Identity.Permission>, PaginationBaseResponse<ViewPermissionResponse>>()
            .ForMember(d => d.CurrentPage, o => o.MapFrom(s => s.CurrentPage))
            .ForMember(d => d.OrderBy, o => o.MapFrom(s => s.OrderBy))
            .ForMember(d => d.PageSize, o => o.MapFrom(s => s.PageSize))
            .ForMember(d => d.TotalItems, o => o.MapFrom(s => s.TotalItems))
            .ForMember(d => d.TotalPages, o => o.MapFrom(s => s.TotalPages))
            .ForMember(d => d.OrderByDesc, o => o.MapFrom(s => s.OrderByDesc))
            .ForMember(d => d.Result, o => o.MapFrom(s => s.Result));
        CreateMap<ViewListPermissionsRequest, ViewListPermissionsQuery>();
    }
}
