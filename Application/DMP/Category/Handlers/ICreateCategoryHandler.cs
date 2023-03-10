using Application.Common.Models;
using Application.DMP.Category.Commands;
using Application.DMP.Category.Commons;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.DMP.Category.Handlers;

public interface ICreateCategoryHandlers : IRequestHandler<CreateCategoryCommand, Result<CategoryResult>>
{
    
}