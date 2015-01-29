namespace ResumePortfolioWebSite.Extensions.IframeOptions
{
    public enum XFrameOption
    {
        /// <summary>
        /// No X-Frame-Options header will be set.
        /// </summary>
        None,
        /// <summary>
        /// This page cannot be rendered in a frame or iframe.
        /// </summary>
        Deny,
        /// <summary>
        /// This page can be rendered in an iframe, but only on the same site.
        /// </summary>
        SameOrigin
    }

    public class XFrameOptionsAttribute : HttpHeaderAttribute
    {
        public XFrameOptionsAttribute(XFrameOption option)
        {
            if (option == XFrameOption.None)
            {
                return;
            }
            Name = "X-Frame-Options";
            Value = option.ToString().ToUpperInvariant();
        }
    }
}