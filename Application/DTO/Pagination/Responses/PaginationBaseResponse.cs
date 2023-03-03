using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Application.Common;

namespace Application.DTO.Pagination.Responses
{
    [ExcludeFromCodeCoverage]
    public class PaginationBaseResponse<T>
    {
        public int CurrentPage { get; set; } = Constants.Pagination.DefaultCurrentPage;

        public int PageSize { get; set; } = Constants.Pagination.DefaultSize;

        public int TotalPages { get; set; } = Constants.Pagination.DefaultTotalPages;

        public int TotalItems { get; set; } = Constants.Pagination.DefaultTotalItems;

        public string? OrderBy { get; set; } = Constants.Pagination.DefaultOrderBy;

        public bool OrderByDesc { get; set; } = Constants.Pagination.DefaultOrderByDesc;

        public List<T> Result { get; set; } = new List<T>();
    }
}