namespace Application.DTO.DMP.Category.Requests;

public class CreateCategoryRequest
{
    public string Name { get; set; }
    public string ShortenUrl { get; set; }
    public int Status { get; set; }
}