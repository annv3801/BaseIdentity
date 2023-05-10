using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.DMP.News.Responses;

public class ViewNewsResponse
{
    public Guid Id { get; set; }
    public string CategoryId { get; set; }
    public IFormFile? Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DMPStatus Status { get; set; }
    public Guid CreateBy { get; set; }
    public DateTime Created { get; set; }
}