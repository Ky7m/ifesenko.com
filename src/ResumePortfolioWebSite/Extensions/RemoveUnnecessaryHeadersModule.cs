using System.Web;

namespace ResumePortfolioWebSite.Extensions
{
    public class RemoveUnnecessaryHeadersModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            // This only works if running in IIS7+ Integrated Pipeline mode
            if (!HttpRuntime.UsingIntegratedPipeline) return;
            context.PreSendRequestHeaders += (sender, e) =>
            {
                var app = sender as HttpApplication;
                if (app != null && app.Context != null)
                {
                    app.Context.Response.Headers.Remove("Server");
                }
            };
        }

        public void Dispose()
        {
            
        }
    }
}