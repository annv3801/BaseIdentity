using Application.Common.Models;
using Application.DMP.Category.Queries;
using Application.DMP.Film.Queries;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.DMP.Film.Responses;
using MediatR;

namespace Application.DMP.Film.Handlers;
public interface IViewFilmHandler: IRequestHandler<ViewFilmQuery, Result<ViewFilmResponse>>
{
    
}
