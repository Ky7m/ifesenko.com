using System.Collections.Generic;
using System.Web.Mvc;

namespace PersonalHomePage.Extensions
{
    public static class JsonResultBuilder
    {
        public static JsonResult ErrorResponse(string message)
        {
            return new JsonResult {Data = new {IsSuccess = false, Message = message}};
        }
        public static JsonResult ErrorResponse(string message, IReadOnlyList<string> errors)
        {
            return new JsonResult {Data = new {IsSuccess = false, Message = message, Errors = errors}};
        }

        public static JsonResult SuccessResponse(string message)
        {
            return new JsonResult {Data = new {IsSuccess = true, Message = message}};
        }
    }
}