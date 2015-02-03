using System.Web;
using WebGrease.Css.Extensions;

namespace ResumePortfolioWebSite.Extensions
{
    public class RemoveUnnecessaryHeadersModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            // This only works if running in IIS7+ Integrated Pipeline mode
            if (!HttpRuntime.UsingIntegratedPipeline)
            {
                return;
            }
            context.PreSendRequestHeaders += (sender, e) =>
            {
                var app = sender as HttpApplication;
                if (app != null && app.Context != null)
                {
                    new[]
                    {
                        "Server",
                        "X-HTML-Minification-Powered-By"
                    }.ForEach(app.Context.Response.Headers.Remove);
                }
            };
        }

        public void Dispose()
        {
            
        }
    }
}