using System;
using ifesenko.com.Infrastructure.Services.Implementation;
using ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Infrastructure.Settings;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using WebMarkupMin.AspNet5;

namespace ifesenko.com
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(appEnv.ApplicationBasePath)
              .AddJsonFile("config.json");

            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();

            _configuration = builder.Build();
            _hostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Disable the default adaptive sampling feature
            var aiOptions = new Microsoft.ApplicationInsights.AspNet.Extensions.ApplicationInsightsServiceOptions();
            aiOptions.EnableAdaptiveSampling = false;
            services.AddApplicationInsightsTelemetry(_configuration, aiOptions);

            // Initialize QuickPulseTelemetryModule
            var module = new QuickPulseTelemetryModule();
            module.Initialize(TelemetryConfiguration.Active);

            // Use and Register QuickPulseTelemetryProcessor
            QuickPulseTelemetryProcessor processor;
            var telemetryBuilder = TelemetryConfiguration.Active.TelemetryProcessorChainBuilder;
            telemetryBuilder.Use(next => {
                processor = new QuickPulseTelemetryProcessor(next);
                module.RegisterTelemetryProcessor(processor);
                return processor;
            });

            // Re-enable Adaptive sampling
            telemetryBuilder.UseAdaptiveSampling();

            // Build the processors
            telemetryBuilder.Build();

            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            services.ConfigureRouting(
              routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });
            //https://github.com/Taritsyn/WebMarkupMin/wiki/WebMarkupMin:-ASP.NET-5
            services.AddWebMarkupMin().AddHtmlMinification();

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

            app.UseIISPlatformHandler();

            app.UseWebMarkupMin();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
