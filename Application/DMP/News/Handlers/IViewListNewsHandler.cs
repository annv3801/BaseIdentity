using Application.Common.Models;
using Application.DMP.Coupon.Queries;
using Application.DMP.News.Queries;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.News.Responses;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.News.Handlers;
public interface IViewListNewsHandler: IRequestHandler<ViewListNewsQuery, Result<PaginationBaseResponse<ViewNewsResponse>>>
{
    
}
