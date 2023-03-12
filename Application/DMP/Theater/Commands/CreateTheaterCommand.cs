using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DMP.Theater.Commons;
using Application.DTO.DMP.Category.Requests;
using Application.DTO.DMP.Theater.Requests;
using MediatR;

namespace Application.DMP.Theater.Commands;

public class CreateTheaterCommand : CreateTheaterRequest, IRequest<Result<TheaterResult>>
{
    
}