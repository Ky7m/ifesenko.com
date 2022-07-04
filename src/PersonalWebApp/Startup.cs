using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using PersonalWebApp.Extensions;
using PersonalWebApp.Middleware;
using PersonalWebApp.Services;
using PersonalWebApp.Settings;

namespace PersonalWebApp;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AppSettings>(_configuration);

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
            });

        services.AddControllersWithViews(options =>
        {
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
            
        services.AddApplicationInsightsTelemetry();
        services.AddApplicationInsightsTelemetryProcessor<CustomTelemetryProcessor>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHsts();
        }

        // health check
        app.Use((context, next) => context.Request.Path.StartsWithSegments("/hc")
            ? context.Response.WriteAsync(string.Empty)
            : next()
        );
            
        app.UseHttpsRedirection();
            
        var rewriteOptions = new RewriteOptions()
            .Add(new RedirectWwwRule());
            
        app.UseRewriter(rewriteOptions);
            
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
                                "centralus-2.in.applicationinsights.azure.com"
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
                                "fonts.gstatic.com",
                                "data:",
                                cdnEndpoint);
                        })
                    .FormActions(x => x.Self())
                    .ImageSources(
                        x =>
                        {
                            x.Self();
                            var customSources = new List<string>
                            {
                                "www.google-analytics.com",
                                "data:",
                                cdnEndpoint
                            };
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

        app.UseStaticFiles(new StaticFileOptions
        {
            OnPrepareResponse = context =>
            {
                var headers = context.Context.Response.GetTypedHeaders();
                headers.CacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromDays(10)
                };
                headers.Set("Access-Control-Allow-Origin", "*");
            }
        });

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}