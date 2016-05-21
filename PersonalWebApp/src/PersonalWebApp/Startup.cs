using System;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<AppSettings>(options => Configuration.GetSection(nameof(AppSettings)).Bind(options));

            services.AddRouting(routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });
            services.AddWebMarkupMin().AddHtmlMinification();

            services.AddMvc(options =>
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
            });

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
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
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
    }
}
