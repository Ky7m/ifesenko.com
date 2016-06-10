using Microsoft.AspNetCore.Mvc;

namespace PersonalWebApp.Controllers
{
    [Route("[controller]")]
    public sealed class ErrorController : Controller
    {
        [Route("{statusCode}")]
        [ResponseCache(CacheProfileName = "ErrorPage")]
        public IActionResult Error(int statusCode)
        {
            return View("Error", statusCode);
        }
    }
}