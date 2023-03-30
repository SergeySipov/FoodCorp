using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiBaseController : ControllerBase
{

}
