using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DTO.DMP.Category.Requests;
using MediatR;

namespace Application.DMP.Category.Commands;

public class CreateCategoryCommand : CreateCategoryRequest, IRequest<Result<CategoryResult>>
{
    
}