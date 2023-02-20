using Microsoft.AspNetCore.Mvc;

namespace FoodCorp.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class ApiBaseController : ControllerBase
{

}
