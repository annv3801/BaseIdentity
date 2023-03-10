using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using MediatR;

namespace Application.DMP.Category.Queries;
[ExcludeFromCodeCoverage]
public class ViewListCategoriesQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewCategoryResponse>>>
{
}
