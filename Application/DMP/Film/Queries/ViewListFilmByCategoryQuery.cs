using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Film.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Film.Queries;
[ExcludeFromCodeCoverage]
public class ViewListFilmByCategoryQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewFilmResponse>>>
{
    public string CategorySlug { get; set; }
}
