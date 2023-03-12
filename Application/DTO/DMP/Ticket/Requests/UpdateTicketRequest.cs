using System.Diagnostics.CodeAnalysis;

namespace Application.DTO.DMP.Ticket.Requests;
[ExcludeFromCodeCoverage]
public class UpdateTicketRequest
{
    public string Name { get; set; }
    public Guid ScheduleId { get; set; }
    public float? Price { get; set; }
    public int Type { get; set; }
}
