using IfesenkoDotCom.Infrastructure;
using Microsoft.AspNet.Mvc;

namespace IfesenkoDotCom.Controllers
{
    [Route("[controller]")]
    public sealed class ErrorController : Controller
    {
        [ResponseCache(CacheProfileName = "Error")]
        [HttpGet("{statusCode}/{status?}")]
        public IActionResult Error(int statusCode, string status)
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