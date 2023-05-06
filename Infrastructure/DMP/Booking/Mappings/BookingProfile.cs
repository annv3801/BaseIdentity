using Application.DMP.Booking.Commands;
using Application.DMP.Booking.Commons;
using Application.DTO.DMP.Booking.Requests;
using AutoMapper;

namespace Infrastructure.DMP.Booking.Mappings;
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<BookingRequest, Domain.Entities.DMP.Booking>().ReverseMap();
        CreateMap<BookingRequest, BookingCommand>();
        CreateMap<Domain.Entities.DMP.Booking, BookingResult>();
    }
}
