using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

public class TestController : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetSmth()
    {
        await Task.Delay(5000);
        return Ok("Smth method");
    }
}
