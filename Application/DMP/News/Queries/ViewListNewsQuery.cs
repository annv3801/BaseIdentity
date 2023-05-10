using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.News.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.News.Queries;
[ExcludeFromCodeCoverage]
public class ViewListNewsQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewNewsResponse>>>
{
}
