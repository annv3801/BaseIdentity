using Application.Common.Models;
using Application.DMP.News.Queries;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.News.Responses;
using MediatR;

namespace Application.DMP.News.Handlers;
public interface IViewNewsHandler: IRequestHandler<ViewNewsQuery, Result<ViewNewsResponse>>
{
    
}
