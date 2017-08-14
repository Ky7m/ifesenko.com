using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Microsoft.ApplicationInsights.Extensibility;
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
using PersonalWebApp.Extensions;
using PersonalWebApp.Middleware;
using PersonalWebApp.Services;
using PersonalWebApp.Settings;

namespace PersonalWebApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddMvc(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());

                options.CacheProfiles.Add("HomePage", new CacheProfile
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 3600,
                    VaryByQueryKeys = new []{ "period" }
                });
                options.CacheProfiles.Add("ErrorPage", new CacheProfile
                {
                    Location = ResponseCacheLocation.Any,
                    Duration = 86400
                });
            });

            services.AddSingleton<IStorageService, InMemoryStorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
                //app.UseExceptionHandler("/error");
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            var rewriteOptions = new RewriteOptions()
                    .AddRedirectToHttpsPermanent()
                    .Add(new RedirectWwwRule());
                app.UseRewriter(rewriteOptions);
            
            var builder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
            builder.Use(next => new SyntheticSourceTelemetryFilter(next));
            builder.Build();

            app.UseResponseCaching();
            app.UseResponseCompression();

            var appSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var cdnEndpoint = appSettings.CdnEndpoint.TrimStart(@"//".ToCharArray());

            app.UseCsp(
                options =>
                {
                    options
                        .UpgradeInsecureRequests()
                        .DefaultSources(x => x.Self())
                        .ChildSources(x =>
                        {
                            x.Self();
                            x.CustomSources("www.youtube.com");
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
                                var customSources = new List<string>
                                {
                                    "www.google-analytics.com",
                                    cdnEndpoint
                                };
                                if (env.IsDevelopment())
                                {
                                    customSources.Add("data:");
                                }
                                x.CustomSources(customSources.ToArray());
                            })
                        .ScriptSources(
                            x =>
                            {
                                x.Self();
                                var customSources = new List<string>
                                {
                                    "az416426.vo.msecnd.net",
                                    "cdnjs.cloudflare.com",
                                    "www.google-analytics.com",
                                    "data:",
                                    cdnEndpoint
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
                                var customSources = new List<string>
                                {
                                    "cdnjs.cloudflare.com",
                                    "fonts.googleapis.com",
                                    cdnEndpoint
                                };
                                x.CustomSources(customSources.ToArray());
                                x.UnsafeInline();
                            });
                });

            app.UseXContentTypeOptions()
                .UseXDownloadOptions()
                .UseXfo(options => options.Deny())
                .UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseStaticFiles();

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","blog")),
                RequestPath = new PathString("/blog"),
                EnableDirectoryBrowsing = false
            });

            if (!env.IsDevelopment())
            {
                app.UseHsts(options => options.MaxAge(days: 18 * 7).IncludeSubdomains().Preload());
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
