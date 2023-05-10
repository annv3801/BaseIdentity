using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.DTO.DMP.News.Requests;

public class CreateNewsRequest
{
    public string CategoryId { get; set; }
    public IFormFile? Image { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DMPStatus Status { get; set; }
}