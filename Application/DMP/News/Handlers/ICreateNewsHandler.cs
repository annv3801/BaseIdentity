using Application.Common.Models;
using Application.DMP.News.Commands;
using Application.DMP.News.Commons;
using MediatR;

namespace Application.DMP.News.Handlers;

public interface ICreateNewsHandlers : IRequestHandler<CreateNewsCommand, Result<NewsResult>>
{
    
}