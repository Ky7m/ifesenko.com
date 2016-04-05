using ifesenko.com.Infrastructure.Extensions;
using Microsoft.AspNet.Mvc;

namespace ifesenko.com.Controllers
{
    [Route("[controller]")]
    public sealed class ErrorController : Controller
    {
        [HttpGet("{statusCode}")]
        [ResponseCache(CacheProfileName = "ErrorPage")]
        public IActionResult Error(int statusCode)
        {
            Response.StatusCode = statusCode;

            ActionResult result;
            if (Request.IsAjaxRequest())
            {
                // This allows us to show errors even in partial views.
                result = PartialView("Error", statusCode);
            }
            else
            {
                result = View("Error", statusCode);
            }

            return result;
        }
    }
}