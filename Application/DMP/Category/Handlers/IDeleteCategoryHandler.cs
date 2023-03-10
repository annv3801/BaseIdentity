using Application.Common.Models;
using Application.DMP.Category.Commands;
using Application.DMP.Category.Commons;
using MediatR;

namespace Application.DMP.Category.Handlers;

public interface IDeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Result<CategoryResult>>
{
    
}