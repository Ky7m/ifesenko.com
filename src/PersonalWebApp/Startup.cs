using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NWebsec.AspNetCore.Mvc.HttpHeaders.Csp;
using PersonalWebApp.Infrastructure.Services.Implementation;
using PersonalWebApp.Infrastructure.Services.Implementation.CloudStorageService;
using PersonalWebApp.Infrastructure.Services.Implementation.HealthService;
using PersonalWebApp.Infrastructure.Services.Interfaces;
using PersonalWebApp.Infrastructure.Settings;
using WebMarkupMin.AspNetCore1;

namespace PersonalWebApp
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _configuration = builder.Build();
            _hostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(_configuration);

            services.AddOptions();
            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            services.AddRouting(routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });

            services.AddWebMarkupMin().AddHtmlMinification();

            services.AddMvc(options =>
            {
                if (!_hostingEnvironment.IsDevelopment())
                {
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
                }

                ConfigureContentSecurityPolicyFilters(_hostingEnvironment, options.Filters);
            });

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton<IHealthService, HealthService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IStorageService, CloudStorageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(_configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}/");

                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next.Invoke();
                    }
                    catch (Exception exception)
                    {
                        var telemetry = app.ApplicationServices.GetService<TelemetryClient>();
                        telemetry?.TrackException(exception);
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                });
                //app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private static void ConfigureContentSecurityPolicyFilters(IHostingEnvironment environment, ICollection<IFilterMetadata> filters)
        {
            filters.Add(new CspAttribute());
            filters.Add(new CspDefaultSrcAttribute
            {
                Self = true
            });

            filters.Add(new CspBaseUriAttribute
            {
                Self = true
            });

            filters.Add(new CspChildSrcAttribute
            {
                Self = true
            });
            filters.Add(new CspConnectSrcAttribute
            {
                CustomSources = string.Join(
                        " ",
                        "dc.services.visualstudio.com"),
                Self = true
            });
            filters.Add(new CspFontSrcAttribute
            {
                CustomSources = string.Join(
                        " ",
                        "cdnjs.cloudflare.com"),
                Self = true
            });
            filters.Add(new CspFormActionAttribute
            {
                Self = true
            });
            filters.Add(
                new CspFrameSrcAttribute
                {
                    Self = false
                });
            filters.Add(
                new CspFrameAncestorsAttribute
                {
                    Self = false
                });
            filters.Add(new CspImgSrcAttribute
            {
                CustomSources = string.Join(
                        " ",
                        "ifesenko.azureedge.net"),
                Self = true,
            });
            filters.Add(new CspScriptSrcAttribute
            {
                CustomSources = string.Join(
                    " ",
                    "az416426.vo.msecnd.net",
                    "cdnjs.cloudflare.com",
                    "ifesenko.azureedge.net"),
                Self = true,
                UnsafeEval = true,
                UnsafeInline = true
            });
            filters.Add(new CspMediaSrcAttribute
            {
                Self = false
            });
            filters.Add(new CspObjectSrcAttribute
            {
                Self = false
            });
            filters.Add(new CspStyleSrcAttribute
            {
                CustomSources = string.Join(
                    " ",
                    "cdnjs.cloudflare.com",
                    "ifesenko.azureedge.net"),
                Self = true,
                UnsafeInline = true
            });

            if (environment.IsDevelopment())
            {
                filters.Add(new CspConnectSrcAttribute
                {
                    CustomSources = string.Join(" ", "localhost:*", "ws://localhost:*")
                });
                filters.Add(new CspImgSrcAttribute {CustomSources = "data:"});
                filters.Add(new CspScriptSrcAttribute {CustomSources = "localhost:*"});
            }
        }
    }
}
