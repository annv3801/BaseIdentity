using Application.DMP.Coupon.Commands;
using Application.DMP.Coupon.Queries;
using Application.DMP.Slider.Commands;
using Application.DMP.Slider.Queries;
using Application.DTO.DMP.Coupon.Requests;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.Slider.Requests;
using Application.DTO.DMP.Slider.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Coupon.Mappings;
public class SliderProfile : Profile
{
    public SliderProfile()
    {
        CreateMap<CreateCouponRequest, CreateCouponCommand>();
        CreateMap<CreateCouponCommand, Domain.Entities.DMP.Coupon>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.MinValue, o => o.MapFrom(s => s.MinValue))
            .ForMember(d => d.MaxValue, o => o.MapFrom(s => s.MaxValue))
            .ForMember(d => d.EffectiveStartDate, o => o.MapFrom(s => s.EffectiveStartDate))
            .ForMember(d => d.EffectiveEndDate, o => o.MapFrom(s => s.EffectiveEndDate))
            .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
            .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
            .ForMember(d => d.RemainingQuantity, o => o.MapFrom(s => s.RemainingQuantity))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<Domain.Entities.DMP.Coupon, ViewCouponResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Code, o => o.MapFrom(s => s.Code))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.MinValue, o => o.MapFrom(s => s.MinValue))
            .ForMember(d => d.MaxValue, o => o.MapFrom(s => s.MaxValue))
            .ForMember(d => d.EffectiveStartDate, o => o.MapFrom(s => s.EffectiveStartDate))
            .ForMember(d => d.EffectiveEndDate, o => o.MapFrom(s => s.EffectiveEndDate))
            .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
            .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
            .ForMember(d => d.RemainingQuantity, o => o.MapFrom(s => s.RemainingQuantity))
            .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));
        CreateMap<ViewListCouponsRequest, ViewListCouponsQuery>();
        CreateMap<ViewListCouponsRequest, ViewCouponQuery>();
    }
}
