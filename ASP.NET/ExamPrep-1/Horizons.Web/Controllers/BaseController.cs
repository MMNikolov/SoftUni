using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;

        protected string? GetUserId()
        {
            string? userId = null!;

            if (IsAuthenticated)
            {
                userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return userId;
        }

    }
}
