using System.Collections.Generic;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NWebsec.AspNetCore.Mvc.HttpHeaders.Csp;
using PersonalWebApp.Infrastructure.Middleware;
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
            services.AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = false;
                    options.AllowCompressionInDevelopmentEnvironment = false;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    var settings = options.MinificationSettings;
                    settings.RemoveRedundantAttributes = true;
                    settings.RemoveHttpProtocolFromAttributes = true;
                    settings.RemoveHttpsProtocolFromAttributes = true;
                })
                .AddHttpCompression();

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

                ConfigureContentSecurityPolicyFilters(options.Filters);
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

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseWebMarkupMin();

            app.UseHeadersMiddleware(new HeadersBuilder()
                .RemoveHeader("Server"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private void ConfigureContentSecurityPolicyFilters(ICollection<IFilterMetadata> filters)
        {
            var cdnUrl = _configuration.GetValue<string>("AppSettings:CdnUrl");
            filters.Add(new CspAttribute());
            filters.Add(new CspDefaultSrcAttribute {Self = true});
            filters.Add(new CspBaseUriAttribute {Self = true});
            filters.Add(new CspChildSrcAttribute {Self = true});
            filters.Add(new CspConnectSrcAttribute {CustomSources = "dc.services.visualstudio.com", Self = true});
            filters.Add(new CspFontSrcAttribute { CustomSources = "cdnjs.cloudflare.com", Self = true});
            filters.Add(new CspFormActionAttribute {Self = true});
            filters.Add(new CspFrameSrcAttribute{Self = false});
            filters.Add(new CspFrameAncestorsAttribute{Self = false});
            filters.Add(new CspImgSrcAttribute {CustomSources = cdnUrl, Self = true});
            filters.Add(new CspScriptSrcAttribute
            {
                CustomSources = string.Join(
                    " ",
                    "az416426.vo.msecnd.net",
                    "cdnjs.cloudflare.com",
                    cdnUrl),
                Self = true,
                UnsafeEval = true,
                UnsafeInline = true
            });
            filters.Add(new CspMediaSrcAttribute {Self = false});
            filters.Add(new CspObjectSrcAttribute {Self = false});
            filters.Add(new CspStyleSrcAttribute
            {
                CustomSources = string.Join(
                    " ",
                    "cdnjs.cloudflare.com",
                    cdnUrl),
                Self = true,
                UnsafeInline = true
            });

            if (_hostingEnvironment.IsDevelopment())
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
