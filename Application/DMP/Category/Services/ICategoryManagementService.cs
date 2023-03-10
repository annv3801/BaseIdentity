using Application.Common.Models;
using Application.DMP.Category.Commons;
using Application.DMP.Category.Queries;
using Application.DTO.DMP.Category.Responses;
using Application.DTO.Pagination.Responses;
using Application.Identity.Role.Commons;

namespace Application.DMP.Category.Services;
public interface ICategoryManagementService
{
    Task<Result<CategoryResult>> CreateCategoryAsync(Domain.Entities.DMP.Category category, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<ViewCategoryResponse>> ViewCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CategoryResult>> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default(CancellationToken));
    Task<Result<CategoryResult>> UpdateCategoryAsync(Domain.Entities.DMP.Category category, CancellationToken cancellationToken = default(CancellationToken));
}
