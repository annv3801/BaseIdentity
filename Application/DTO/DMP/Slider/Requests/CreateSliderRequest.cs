using Microsoft.AspNetCore.Http;

namespace Application.DTO.DMP.Slider.Requests;

public class CreateSliderRequest
{
    public IFormFile? Image { get; set; }
    public int Status { get; set; }
}