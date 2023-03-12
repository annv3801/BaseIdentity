using Application.Common.Models;
using Application.DMP.Seat.Queries;
using Application.DTO.DMP.Seat.Responses;
using MediatR;

namespace Application.DMP.Seat.Handlers;
public interface IViewSeatHandler: IRequestHandler<ViewSeatQuery, Result<ViewSeatResponse>>
{
    
}
