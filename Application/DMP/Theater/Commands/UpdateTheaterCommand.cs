using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DMP.Theater.Commons;
using Application.DTO.DMP.Category.Requests;
using Application.DTO.DMP.Theater.Requests;
using MediatR;

namespace Application.DMP.Theater.Commands;
[ExcludeFromCodeCoverage]
public class UpdateTheaterCommand : UpdateTheaterRequest, IRequest<Result<TheaterResult>>
{
    public Guid Id { get; set; }
}
