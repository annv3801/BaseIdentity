using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Film.Responses;
using MediatR;

namespace Application.DMP.Film.Queries;
[ExcludeFromCodeCoverage]
public class ViewFilmQuery : IRequest<Result<ViewFilmResponse>>
{
    public Guid Id { get; set; }
}
