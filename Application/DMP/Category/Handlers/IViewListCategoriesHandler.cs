using Application.Common.Models;
using Application.DMP.Category.Queries;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.Pagination.Responses;
using Application.DTO.Role.Responses;
using Application.Identity.Role.Queries;
using MediatR;

namespace Application.DMP.Category.Handlers;
public interface IViewListCategoriesHandler: IRequestHandler<ViewListCategoriesQuery, Result<PaginationBaseResponse<ViewCategoryResponse>>>
{
    
}
