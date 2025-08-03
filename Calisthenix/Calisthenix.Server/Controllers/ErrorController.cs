using Microsoft.AspNetCore.Mvc;

namespace Calisthenix.Server.Controllers
{
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public IActionResult HandleError()
        {
            return Problem(
                detail: "Something went wrong. Please try again later.",
                statusCode: 500,
                title: "Server Error");
        }
    }
}
