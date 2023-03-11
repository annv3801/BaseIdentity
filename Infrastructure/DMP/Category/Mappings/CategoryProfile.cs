using Application.DMP.Category.Commands;
using Application.DMP.Category.Queries;
using Application.DTO.DMP.Category.Requests;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.Pagination.Responses;
using Application.DTO.Permission.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Category.Mappings;
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>();

        CreateMap<CreateCategoryCommand, Domain.Entities.DMP.Category>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<Domain.Entities.DMP.Category, ViewCategoryResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
                    .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>();
        CreateMap<UpdateCategoryCommand, Domain.Entities.DMP.Category>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ShortenUrl, o => o.MapFrom(s => s.ShortenUrl))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<ViewListCategoriesRequest, ViewListCategoriesQuery>();
        CreateMap<ViewListCategoriesRequest, ViewCategoryQuery>();
    }
}
