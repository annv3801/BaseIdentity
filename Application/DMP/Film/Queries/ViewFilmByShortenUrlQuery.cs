using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Film.Responses;
using MediatR;

namespace Application.DMP.Film.Queries;
[ExcludeFromCodeCoverage]
public class ViewFilmByShortenUrlQuery : IRequest<Result<ViewFilmResponse>>
{
    public string ShortenUrl { get; set; }
}
