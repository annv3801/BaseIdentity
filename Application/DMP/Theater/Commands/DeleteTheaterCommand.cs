using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DMP.Theater.Commons;
using MediatR;

namespace Application.DMP.Theater.Commands;

public class DeleteTheaterCommand : IRequest<Result<TheaterResult>>
{
    public Guid Id { get; set; }
}