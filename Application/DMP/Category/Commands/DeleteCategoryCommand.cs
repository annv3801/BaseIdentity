using Application.Common.Models;
using Application.DMP.Category.Commons;
using MediatR;

namespace Application.DMP.Category.Commands;

public class DeleteCategoryCommand : IRequest<Result<CategoryResult>>
{
    public Guid Id { get; set; }
}