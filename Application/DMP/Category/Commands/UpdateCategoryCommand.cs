using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DTO.DMP.Category.Requests;
using Application.DTO.Role.Requests;
using Application.Identity.Role.Commons;
using MediatR;

namespace Application.DMP.Category.Commands;
[ExcludeFromCodeCoverage]
public class UpdateCategoryCommand : UpdateCategoryRequest, IRequest<Result<CategoryResult>>
{
    public Guid Id { get; set; }
}
