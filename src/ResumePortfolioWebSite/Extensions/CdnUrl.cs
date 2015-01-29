using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Optimization;

namespace ResumePortfolioWebSite.Extensions
{
    public class CdnUrl
    {
        public static string Content(string url)
        {
            if (String.IsNullOrWhiteSpace(url))
            {
                return String.Empty;
            }

            string absolutepath = VirtualPathUtility.ToAbsolute(Path.Combine("~", url).Replace(@"\", @"/"));
            if (!BundleTable.EnableOptimizations)
            {
                return absolutepath;
            }

            if (HttpRuntime.Cache[url] == null)
            {
                string physicalPath = HostingEnvironment.MapPath(absolutepath);
                if (String.IsNullOrEmpty(physicalPath) || !File.Exists(physicalPath))
                {
                    return url;
                }

                var date = File.GetLastWriteTime(physicalPath);
                var result = Path.Combine(ConfigurationManager.AppSettings.Get("CdnUrl"), url).Replace(@"\",@"/");// + "?v=" + date.Ticks;

                HttpRuntime.Cache.Insert(url, result, new CacheDependency(physicalPath));
            }

            return HttpRuntime.Cache[url] as string;
        }
    }
}