using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Theater.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Theater.Queries;
[ExcludeFromCodeCoverage]
public class ViewListTheatersQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewTheaterResponse>>>
{
}
