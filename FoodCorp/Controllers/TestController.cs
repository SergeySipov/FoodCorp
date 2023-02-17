using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public IActionResult Test()
    {
        return Ok("everything works!");
    }
}
