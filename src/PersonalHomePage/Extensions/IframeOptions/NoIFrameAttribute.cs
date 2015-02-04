using System.Web.Mvc;

namespace PersonalHomePage.Extensions.IframeOptions
{
    /// <summary>
    /// Prevents a page from being displayed within a frame or iframe element,
    /// by adding a HTTP response header: X-Frame-Options: DENY
    /// This can help to avoid clickjacking attacks, by ensuring your content is not embedded into other sites.
    /// </summary>
    public class NoIFrameAttribute : XFrameOptionsAttribute
    {
        public NoIFrameAttribute() : base(XFrameOption.Deny)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Abort if an [IFrame] attribute is applied to controller or action
            var attributesFromController = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof (IFrameAttribute), true);
            var attributesFromAction = filterContext.ActionDescriptor.GetCustomAttributes(typeof (IFrameAttribute), true);
            if (attributesFromController.Length > 0 || attributesFromAction.Length > 0)
            {
                return;
            }

            base.OnActionExecuted(filterContext);
        }
    }
}