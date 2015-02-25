using System;
using System.Linq;
using System.Web.Mvc;
using PersonalHomePage.Extensions;
using PersonalHomePage.Extensions.IframeOptions;
using PersonalHomePage.Models;
using WebMarkupMin.Mvc.ActionFilters;

namespace PersonalHomePage.Controllers
{
    public class HomeController : Controller
    {
        [CompressContent,
         MinifyHtml,
         OutputCache(CacheProfile = "HomePage"),
         NoIFrame]
        public ActionResult Index()
        {
            return View(new HomeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SendEmailMessage(EmailMessageModel emailMessage)
        {
			if (ModelState.IsValid)
			{
				// Attempt to send email
				try
				{
				
					return Json(new { success = true });
				}
				catch (Exception exception)
				{
					return JsonResultBuilder.ErrorResponse(exception.Message);
				}
			}
			return Json(new
			{
				success = false,
				errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
								.Select(m => m.ErrorMessage).ToArray()
			});
			return JsonResultBuilder.SuccessResponse("Thank you very much for your email.");
        }
    }
}