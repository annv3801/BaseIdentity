using Application.DTO.Pagination.Responses;
using AutoMapper;

namespace Infrastructure.Common.Mappings;
//[ExcludeFromCodeCoverage]
public class PaginationProfile : Profile
{
    public PaginationProfile()
    {
        CreateMap(typeof(PaginationBaseResponse<>), typeof(PaginationBaseResponse<>))
            .ForMember("CurrentPage", o => o.MapFrom("CurrentPage"))
            .ForMember("OrderBy", o => o.MapFrom("OrderBy"))
            .ForMember("PageSize", o => o.MapFrom("PageSize"))
            .ForMember("TotalItems", o => o.MapFrom("TotalItems"))
            .ForMember("TotalPages", o => o.MapFrom("TotalPages"))
            .ForMember("OrderByDesc", o => o.MapFrom("OrderByDesc"))
            .ForMember("Result", o => o.MapFrom("Result"));
    }
}
