using Application.DMP.Ticket.Commands;
using Application.DMP.Ticket.Queries;
using Application.DTO.DMP.Ticket.Requests;
using Application.DTO.DMP.Ticket.Responses;
using AutoMapper;

namespace Infrastructure.DMP.Ticket.Mappings;
public class TicketProfile : Profile
{
    public TicketProfile()
    {
        CreateMap<CreateTicketRequest, CreateTicketCommand>();

        CreateMap<CreateTicketCommand, Domain.Entities.DMP.Ticket>()
            .ForMember(d => d.Id, o => o.MapFrom(s => Guid.NewGuid()))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ScheduleId, o => o.MapFrom(s => s.ScheduleId))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type));
        CreateMap<Domain.Entities.DMP.Ticket, ViewTicketResponse>()
                    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                    .ForMember(d => d.ScheduleId, o => o.MapFrom(s => s.ScheduleId))
                    .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                    .ForMember(d => d.Type, o => o.MapFrom(s => s.Type));
        CreateMap<UpdateTicketRequest, UpdateTicketCommand>();
        CreateMap<UpdateTicketCommand, Domain.Entities.DMP.Ticket>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.ScheduleId, o => o.MapFrom(s => s.ScheduleId))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type));
        CreateMap<ViewListTicketsRequest, ViewListTicketsQuery>();
        CreateMap<ViewListTicketsRequest, ViewTicketQuery>();
    }
}
