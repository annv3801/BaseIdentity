using Application.Common.Models;
using Application.DMP.Category.Commands;
using Application.DMP.Category.Commons;
using Application.Identity.Role.Commands;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.DMP.Category.Handlers;
public interface IUpdateCategoryHandler: IRequestHandler<UpdateCategoryCommand, Result<CategoryResult>>
{
}
