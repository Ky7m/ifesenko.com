using System.Text;
using System.Web.Mvc;

namespace PersonalHomePage.Extensions
{
    public static class AnalyticsHelper
    {
        public static MvcHtmlString GetGoogleAnalyticsScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script>(function(G,o,O,g,l){G.GoogleAnalyticsObject=O;G[O]||(G[O]=function()");
            sb.Append("{(G[O].q=G[O].q||[]).push(arguments)});G[O].l=+new ");
            sb.Append("Date;g=o.createElement('script'),l=o.scripts[0];g.src='//www.google-");
            sb.Append("analytics.com/analytics.js';l.parentNode.insertBefore(g,l)}");
            sb.Append("(this,document,'ga'));ga('create','UA-58923658-1', 'auto');ga('send','pageview')</script>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString GetYandexMetricsScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>(function (d, w, c) { (w[c] = w[c] || []).push(function() { try { w.yaCounter28078209 = new Ya.Metrika({id:28078209, webvisor:true,");
            sb.Append("clickmap:true, trackLinks:true, accurateTrackBounce:true}); } catch(e) { } }); var n = d.getElementsByTagName('script')[0], s = d.createElement('script'), f = ");
            sb.Append("function () { n.parentNode.insertBefore(s, n); }; s.type = 'text/javascript'; s.async = true; s.src = (d.location.protocol == 'https:' ? 'https:' : 'http:') + ");
            sb.Append("'//mc.yandex.ru/metrika/watch.js'; if (w.opera == '[object Opera]') { d.addEventListener('DOMContentLoaded', f, false); } else { f(); } })(document, window, ");
            sb.Append("'yandex_metrika_callbacks');</script><noscript><div><img src='//mc.yandex.ru/watch/28078209' style='position:absolute; left:-9999px;' alt='' /></div></noscript>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString GetAppInsightsScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type='text/javascript'>var appInsights=window.appInsights||function(config){function s(config){t[config]=function(){var i=arguments;t.queue.push(function(){t[config].apply(t,i)})}}var t={config:config},r=document,f=window,e='script',");
            sb.Append("o=r.createElement(e),i,u;for(o.src=config.url||'//az416426.vo.msecnd.net/scripts/a/ai.0.js',r.getElementsByTagName(e)[0].parentNode.appendChild(o),t.cookie=r.cookie,t.queue=[],i=['Event','Exception','Metric','PageView','Trace'];i.length;)");
            sb.Append("s('track'+i.pop());return config.disableExceptionTracking||(i='onerror',s('_'+i),u=f[i],f[i]=function(config,r,f,e,o){var s=u&&u(config,r,f,e,o);return s!==!0&&t['_'+i](config,r,f,e,o),s}),t}");
            sb.Append("({instrumentationKey:'1f26649d-7b51-452a-af4a-514e08ffa75d'});window.appInsights=appInsights;appInsights.trackPageView();</script>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}