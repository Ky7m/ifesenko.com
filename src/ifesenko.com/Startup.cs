using System;
using ifesenko.com.Infrastructure.Services.Implementation;
using ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Infrastructure.Settings;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using WebMarkupMin.AspNet5;

namespace ifesenko.com
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _configuration = builder.Build();
            _hostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(_configuration);

            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            services.ConfigureRouting(
              routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });
            //https://github.com/Taritsyn/WebMarkupMin/wiki/WebMarkupMin:-ASP.NET-5
            //services.AddWebMarkupMin().AddHtmlMinification();

            services.AddMvc(options =>
            {
                if (_hostingEnvironment.IsDevelopment())
                {
                    return;
                }
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

            services.AddSingleton<IHealthService, HealthService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IStorageService, CloudStorageService>();
        }

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
                    catch(Exception exception)
                    {
                        var telemetry = app.ApplicationServices.GetService<TelemetryClient>();
                        telemetry?.TrackException(exception);
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }
                });
            }

            if (env.IsProduction())
            {
                app.UseApplicationInsightsExceptionTelemetry();
            }

            //app.UseWebMarkupMin();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
