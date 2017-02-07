using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NWebsec.AspNetCore.Middleware;
using PersonalWebApp.Middleware;
using PersonalWebApp.Services.Implementation.CloudStorageService;
using PersonalWebApp.Services.Interfaces;
using PersonalWebApp.Settings;
using WebMarkupMin.AspNetCore1;

namespace PersonalWebApp
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(_configuration);

            services.AddOptions();
            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            services.AddRouting(routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });

            services.AddResponseCaching();

            services.AddResponseCompression(
                    options =>
                    {
                        options.EnableForHttps = true;
                    })
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            // Add WebMarkupMin services to the services container.
            services.AddWebMarkupMin(options =>
               {
                   //options.AllowMinificationInDevelopmentEnvironment = true;
                   //options.AllowCompressionInDevelopmentEnvironment = true;
                   options.DisablePoweredByHttpHeaders = true;
               })
               .AddHtmlMinification(options =>
               {
                   var settings = options.MinificationSettings;
                   settings.RemoveRedundantAttributes = true;
                   settings.RemoveHttpProtocolFromAttributes = true;
                   settings.RemoveHttpsProtocolFromAttributes = true;
               });

            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());

                options.CacheProfiles.Add("HomePage", new CacheProfile
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 3600
                });
                options.CacheProfiles.Add("ErrorPage", new CacheProfile
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 86400
                });
            });

            // Turn off compaction on memory pressure as it results in things being evicted during the priming of the
            // cache on application start.
            services.AddMemoryCache(options => options.CompactOnMemoryPressure = false);
            services.AddSingleton<CachedWebRootFileProvider>();
            services.AddSingleton<IConfigureOptions<StaticFileOptions>, StaticFileOptionsSetup>();
            services.AddSingleton<IStartupFilter, AppStart>();

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton<IStorageService, CloudStorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(_configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                var rewriteOptions = new RewriteOptions()
                    .AddRedirectToHttpsPermanent()
                    .Add(new RedirectWwwRule());
                app.UseRewriter(rewriteOptions);
            }

            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseCsp(
                options =>
                {
                    options
                        .UpgradeInsecureRequests()
                        .DefaultSources(x => x.Self())
                        .ChildSources(x =>
                        {
                            x.Self();
                            x.CustomSources(
                                "www.youtube.com",
                                "disqus.com");
                        })
                        .ConnectSources(
                            x =>
                            {
                                x.Self();
                                var customSources = new List<string>
                                {
                                    "dc.services.visualstudio.com"
                                };
                                if (env.IsDevelopment())
                                {
                                    customSources.Add("localhost:*");
                                    customSources.Add("ws://localhost:*");
                                }
                                x.CustomSources(customSources.ToArray());
                            })
                        .FontSources(
                            x =>
                            {
                                x.Self();
                                x.CustomSources(
                                    "cdnjs.cloudflare.com",
                                    "fonts.googleapis.com",
                                    "fonts.gstatic.com");
                            })
                        .FormActions(x => x.Self())
                        .ImageSources(
                            x =>
                            {
                                x.Self();
                                x.CustomSources(
                                    "referrer.disqus.com",
                                    "a.disquscdn.com",
                                    "www.google-analytics.com");
                                if (env.IsDevelopment())
                                {
                                    x.CustomSources("data:");
                                }
                            })
                        .ScriptSources(
                            x =>
                            {
                                x.Self();
                                var customSources = new List<string>
                                {
                                    "az416426.vo.msecnd.net",
                                    "cdnjs.cloudflare.com",
                                    "ifesenko.disqus.com",
                                    "a.disquscdn.com",
                                    "www.google-analytics.com",
                                    "data:"
                                };
                                if (env.IsDevelopment())
                                {
                                    customSources.Add("localhost:*");
                                }
                                x.CustomSources(customSources.ToArray());
                                x.UnsafeEval();
                                x.UnsafeInline();
                            })
                        .StyleSources(
                            x =>
                            {
                                x.Self();
                                x.CustomSources(
                                    "cdnjs.cloudflare.com",
                                    "fonts.googleapis.com",
                                    "a.disquscdn.com");
                                x.UnsafeInline();
                            });
                });

            app.UseXContentTypeOptions()
                .UseXDownloadOptions()
                .UseXfo(options => options.Deny())
                .UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","blog")),
                RequestPath = new PathString("/blog"),
                EnableDirectoryBrowsing = false
            });

            app.UseWebMarkupMin();

            app.UseHeadersMiddleware(new HeadersBuilder()
                .RemoveHeader("Server"));

            app.UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
