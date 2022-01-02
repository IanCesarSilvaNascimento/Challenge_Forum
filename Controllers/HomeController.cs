using ForumApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controller
{

    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {

        [HttpGet("")]
        public IActionResult Get()
            => Ok();
    }
}