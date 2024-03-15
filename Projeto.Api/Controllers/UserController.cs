using Microsoft.AspNetCore.Mvc;

namespace Projeto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{


    [HttpGet("/")]
    public string Get()
    {
        return "Api rodando";
    }
}
