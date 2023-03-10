using Application.Common.Models;
using Application.DMP.Category.Queries;
using Application.DTO.DMP.Category.Responses;
using MediatR;

namespace Application.DMP.Category.Handlers;
public interface IViewCategoryHandler: IRequestHandler<ViewCategoryQuery, Result<ViewCategoryResponse>>
{
    
}
