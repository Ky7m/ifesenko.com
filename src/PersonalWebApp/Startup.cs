using System.Collections.Generic;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NWebsec.AspNetCore.Middleware;
using PersonalWebApp.Infrastructure.Middleware;
using PersonalWebApp.Infrastructure.Services.Implementation;
using PersonalWebApp.Infrastructure.Services.Implementation.CloudStorageService;
using PersonalWebApp.Infrastructure.Services.Implementation.HealthService;
using PersonalWebApp.Infrastructure.Services.Interfaces;
using PersonalWebApp.Infrastructure.Settings;
//using WebMarkupMin.AspNetCore1;

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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var aiOptions = new ApplicationInsightsServiceOptions
            {
                EnableQuickPulseMetricStream = false
            };
            services.AddApplicationInsightsTelemetry(_configuration, aiOptions);

            services.AddOptions();
            services.Configure<AppSettings>(_configuration.GetSection(nameof(AppSettings)));

            services.AddRouting(routeOptions =>
              {
                  routeOptions.AppendTrailingSlash = true;
                  routeOptions.LowercaseUrls = true;
              });

            // Add WebMarkupMin services to the services container.
            //services.AddWebMarkupMin(options =>
            //    {
            //        options.AllowMinificationInDevelopmentEnvironment = false;
            //        options.AllowCompressionInDevelopmentEnvironment = false;
            //        options.DisablePoweredByHttpHeaders = true;
            //    })
            //    .AddHtmlMinification(options =>
            //    {
            //        var settings = options.MinificationSettings;
            //        settings.RemoveRedundantAttributes = true;
            //        settings.RemoveHttpProtocolFromAttributes = true;
            //        settings.RemoveHttpsProtocolFromAttributes = true;
            //    })
            //    .AddHttpCompression();

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

            var cdnUrl = _configuration.GetValue<string>("AppSettings:CdnUrl");

            app.UseCsp(
                options =>
                {
                    options
                        .DefaultSources(x => x.Self())
                        .ChildSources(x => x.Self())
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
                                x.CustomSources("cdnjs.cloudflare.com");
                            })
                        .FormActions(x => x.Self())
                        .ImageSources(
                            x =>
                            {
                                x.Self();
                                if (env.IsDevelopment())
                                {
                                    x.CustomSources("data:");
                                }
                                x.CustomSources(cdnUrl);
                            })
                        .ScriptSources(
                            x =>
                            {
                                x.Self();
                                var customSources = new List<string>
                                {
                                    "az416426.vo.msecnd.net",
                                    "cdnjs.cloudflare.com",
                                    cdnUrl
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
                                x.CustomSources("cdnjs.cloudflare.com", cdnUrl);
                                x.UnsafeInline();
                            });
                });

            app.UseXContentTypeOptions()
                .UseXDownloadOptions()
                .UseXfo(options => options.Deny());

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            //app.UseWebMarkupMin();

            app.UseHeadersMiddleware(new HeadersBuilder()
                .RemoveHeader("Server"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
