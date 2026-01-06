namespace Calisthenix.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;

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
