using FoodCorp.BusinessLogic.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly IProductService _productService;

    public TestController(IProductService productService)
    {
        _productService = productService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Test()
    {
        var result = await _productService.GetAllProductsAsync();
        var b = _productService.GetProductByIdAsync(3016);

        return Ok(result);
    }
}
