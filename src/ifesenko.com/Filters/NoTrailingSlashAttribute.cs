using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;

namespace IfesenkoDotCom.Filters
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class NoTrailingSlashAttribute : AuthorizationFilterAttribute
    {
        private const char SlashCharacter = '/';
        public override void OnAuthorization(AuthorizationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var path = context.HttpContext.Request.Path;
            if (path.HasValue)
            {
                if (path.Value[path.Value.Length - 1] == SlashCharacter)
                {
                    this.HandleTrailingSlashRequest(context);
                }
            }
        }

        protected virtual void HandleTrailingSlashRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpNotFoundResult();
        }
    }
}