using System.Web.Mvc;

namespace PersonalHomePage.Extensions
{
    public class JsonResponseBuilder
    {
        public static JsonResult ErrorResponse(string error)
        {
            return new JsonResult {Data = new {IsSuccess = false, Message = error}};
        }

        public static JsonResult SuccessResponse(string success)
        {
            return new JsonResult {Data = new {IsSuccess = true, Message = success}};
        }
    }
}