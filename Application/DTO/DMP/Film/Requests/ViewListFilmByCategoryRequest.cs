using System.Diagnostics.CodeAnalysis;
using Application.DTO.Pagination.Requests;

namespace Application.DTO.DMP.Film.Requests;
[ExcludeFromCodeCoverage]
public class ViewListFilmByCategoryRequest : PaginationBaseRequest
{
    public string CategorySlug { get; set; }
}
