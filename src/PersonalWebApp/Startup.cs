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
using PersonalWebApp.Services;
using PersonalWebApp.Settings;

namespace PersonalWebApp;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<AppSettings>(configuration);

        services.AddRouting(routeOptions =>
        {
            routeOptions.AppendTrailingSlash = true;
            routeOptions.LowercaseUrls = true;
        });

        services.AddResponseCaching();

        services.AddResponseCompression(
            options => { options.EnableForHttps = true; });

        services.AddControllersWithViews(options =>
        {
            options.CacheProfiles.Add("HomePage", new CacheProfile
            {
                Location = ResponseCacheLocation.Any,
                Duration = 3600,
                VaryByQueryKeys = ["period"]
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

        app.UseResponseCaching();
        app.UseResponseCompression();

        var appSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
        var cdnEndpoint = appSettings.CdnEndpoint.TrimStart(@"//".ToCharArray());

        var policyCollection = new HeaderPolicyCollection()
            .AddContentSecurityPolicy(builder =>
            {
                builder.AddUpgradeInsecureRequests();
                builder.AddBlockAllMixedContent();

                builder.AddDefaultSrc()
                    .Self();

                var connectSrcList = new List<string>
                {
                    "centralus-2.in.applicationinsights.azure.com",
                    "*.google-analytics.com"
                };
                if (env.IsDevelopment())
                {
                    connectSrcList.Add("localhost:*");
                    connectSrcList.Add("ws://localhost:*");
                }

                builder.AddConnectSrc()
                    .Self()
                    .From(connectSrcList);

                builder.AddFontSrc()
                    .Self()
                    .From(["cdnjs.cloudflare.com", "fonts.googleapis.com", "fonts.gstatic.com", "data:", cdnEndpoint]);

                builder.AddObjectSrc() // object-src 'none'
                    .None();

                builder.AddFormAction()
                    .Self();

                builder.AddImgSrc()
                    .Self()
                    .From(["*.google-analytics.com", "www.googletagmanager.com", "data:", cdnEndpoint])
                    .OverHttps();

                var scriptSrcList = new List<string>
                {
                    "az416426.vo.msecnd.net",
                    "cdnjs.cloudflare.com",
                    "js.monitor.azure.com",
                    "*.google-analytics.com",
                    "www.googletagmanager.com",
                    "data:",
                    cdnEndpoint
                };
                if (env.IsDevelopment())
                {
                    scriptSrcList.Add("localhost:*");
                }

                builder.AddScriptSrc()
                    .Self()
                    .From(scriptSrcList)
                    .UnsafeInline()
                    .UnsafeEval()
                    .ReportSample();

                builder.AddStyleSrc()
                    .Self()
                    .UnsafeInline()
                    .From(["cdnjs.cloudflare.com", "fonts.googleapis.com", cdnEndpoint])
                    .StrictDynamic();

                builder.AddMediaSrc()
                    .OverHttps();

                builder.AddFrameAncestors()
                    .None();

                builder.AddBaseUri()
                    .Self();
            });

        app.UseSecurityHeaders(policyCollection);

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