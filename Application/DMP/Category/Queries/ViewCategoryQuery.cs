using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Category.Responses;
using MediatR;

namespace Application.DMP.Category.Queries;
[ExcludeFromCodeCoverage]
public class ViewCategoryQuery : IRequest<Result<ViewCategoryResponse>>
{
    public Guid Id { get; set; }
}
