using Application.Common.Interfaces;
using Application.DTO.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Areas.DMP.Controllers;

public class PaymentUrlController : ControllerBase
{
    private readonly IVnPayService _vnPayService;

    public PaymentUrlController(IVnPayService vnPayService)
    {
        _vnPayService = vnPayService;
    }
    
    public IActionResult CreatePaymentUrl(PaymentInformationModel model)
    {
        var test = new PaymentInformationModel()
        {
            OrderType = "other",
            Amount = 120000,
            OrderDescription = "Test 1",
            Name = "An"
        };
        var url = _vnPayService.CreatePaymentUrl(test);
        return Redirect(url);
    }
}