using System.Text;
using System.Web.Mvc;

namespace ResumePortfolioWebSite.Extensions
{
	public static class AnalyticsHelper
	{
		public static MvcHtmlString GetAnalyticsScript()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<script>(function(G,o,O,g,l){G.GoogleAnalyticsObject=O;G[O]||(G[O]=function()");
			sb.Append("{(G[O].q=G[O].q||[]).push(arguments)});G[O].l=+new ");
			sb.Append("Date;g=o.createElement('script'),l=o.scripts[0];g.src='//www.google-");
			sb.Append("analytics.com/analytics.js';l.parentNode.insertBefore(g,l)}");
			sb.Append("(this,document,'ga'));ga('create','UA-58923658-1', 'auto');ga('send','pageview')</script>");
			return MvcHtmlString.Create(sb.ToString());
		}
	}
}