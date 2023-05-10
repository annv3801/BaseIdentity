using Application.Common.Models;
using Application.DMP.News.Commons;
using Application.DTO.DMP.News.Requests;
using MediatR;

namespace Application.DMP.News.Commands;

public class CreateNewsCommand : CreateNewsRequest, IRequest<Result<NewsResult>>
{
    
}