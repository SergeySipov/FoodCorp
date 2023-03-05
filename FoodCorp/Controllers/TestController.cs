using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

public class TestController : ApiBaseController
{
    [HttpGet]
    [Authorize]
    public IActionResult Test()
    {
        return Ok("authorized endpoint");
    }
}
