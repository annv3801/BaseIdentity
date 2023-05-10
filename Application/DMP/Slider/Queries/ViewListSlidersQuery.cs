using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Slider.Responses;
using Application.DTO.Pagination.Requests;
using Application.DTO.Pagination.Responses;
using MediatR;

namespace Application.DMP.Slider.Queries;
[ExcludeFromCodeCoverage]
public class ViewListSlidersQuery : PaginationBaseRequest, IRequest<Result<PaginationBaseResponse<ViewSliderResponse>>>
{
}
