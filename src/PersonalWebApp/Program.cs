using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using PersonalWebApp.Extensions;
using PersonalWebApp.Services;
using PersonalWebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddRouting(routeOptions =>
{
    routeOptions.AppendTrailingSlash = true;
    routeOptions.LowercaseUrls = true;
});

builder.Services.AddResponseCaching();
builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddControllersWithViews(options =>
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

builder.Services.AddSingleton<IStorageService, InMemoryStorageService>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddApplicationInsightsTelemetryProcessor<CustomTelemetryProcessor>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
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

var appSettings = builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
var cdnEndpoint = appSettings!.CdnEndpoint.TrimStart(@"//".ToCharArray());

var policyCollection = new HeaderPolicyCollection()
    .AddContentSecurityPolicy(cspBuilder =>
    {
        cspBuilder.AddUpgradeInsecureRequests();
        cspBuilder.AddBlockAllMixedContent();

        cspBuilder.AddDefaultSrc().Self();

        var connectSrcList = new List<string>
        {
            "centralus-2.in.applicationinsights.azure.com",
            "*.google-analytics.com"
        };
        if (app.Environment.IsDevelopment())
        {
            connectSrcList.Add("localhost:*");
            connectSrcList.Add("ws://localhost:*");
        }

        cspBuilder.AddConnectSrc().Self().From(connectSrcList);

        cspBuilder.AddFontSrc()
            .Self()
            .From(["cdn.jsdelivr.net", "cdnjs.cloudflare.com", "fonts.googleapis.com", "fonts.gstatic.com", "data:", cdnEndpoint]);

        cspBuilder.AddObjectSrc().None();
        cspBuilder.AddFormAction().Self();

        cspBuilder.AddImgSrc()
            .Self()
            .From(["*.google-analytics.com", "www.googletagmanager.com", "data:", cdnEndpoint])
            .OverHttps();

        var scriptSrcList = new List<string>
        {
            "az416426.vo.msecnd.net",
            "cdn.jsdelivr.net",
            "cdnjs.cloudflare.com",
            "js.monitor.azure.com",
            "*.google-analytics.com",
            "www.googletagmanager.com",
            "data:",
            cdnEndpoint
        };
        if (app.Environment.IsDevelopment())
        {
            scriptSrcList.Add("localhost:*");
        }

        cspBuilder.AddScriptSrc()
            .Self()
            .From(scriptSrcList)
            .UnsafeInline()
            .UnsafeEval()
            .ReportSample();

        cspBuilder.AddStyleSrc()
            .Self()
            .UnsafeInline()
            .From(["cdn.jsdelivr.net", "cdnjs.cloudflare.com", "fonts.googleapis.com", cdnEndpoint])
            .StrictDynamic();

        cspBuilder.AddMediaSrc().OverHttps();
        cspBuilder.AddFrameAncestors().None();
        cspBuilder.AddBaseUri().Self();
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
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
