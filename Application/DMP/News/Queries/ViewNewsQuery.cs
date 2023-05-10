using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using Application.DTO.DMP.Coupon.Responses;
using Application.DTO.DMP.News.Responses;
using MediatR;

namespace Application.DMP.News.Queries;
[ExcludeFromCodeCoverage]
public class ViewNewsQuery : IRequest<Result<ViewNewsResponse>>
{
    public Guid Id { get; set; }
}

