using Application.Common.Models;
using Application.DMP.Theater.Commands;
using Application.DMP.Theater.Commons;
using MediatR;

namespace Application.DMP.Theater.Handlers;

public interface IDeleteTheaterHandler : IRequestHandler<DeleteTheaterCommand, Result<TheaterResult>>
{
    
}