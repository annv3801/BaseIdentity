using Application.Common.Models;
using Application.DMP.Theater.Queries;
using Application.DTO.DMP.Theater.Responses;
using MediatR;

namespace Application.DMP.Theater.Handlers;
public interface IViewTheaterHandler: IRequestHandler<ViewTheaterQuery, Result<ViewTheaterResponse>>
{
    
}
