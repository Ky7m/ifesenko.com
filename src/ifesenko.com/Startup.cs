using ifesenko.com.Filters;
using ifesenko.com.Services.Implementation;
using ifesenko.com.Services.Implementation.CloudStorageService;
using ifesenko.com.Services.Implementation.HealthService;
using ifesenko.com.Services.Interfaces;
using ifesenko.com.Settings;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;

namespace ifesenko.com
{
    public class Startup
    {
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.Configure<CacheProfileSettings>(Configuration.GetSection(nameof(CacheProfileSettings)));
            services.Configure<SitemapSettings>(Configuration.GetSection(nameof(SitemapSettings)));
            services.Configure<EmailServiceSettings>(Configuration.GetSection(nameof(EmailServiceSettings)));

            //services.AddCaching();
            // services.AddTransient<IDistributedCache, RedisCache>();


            RouteOptions routeOptions = null;
            services.ConfigureRouting(
                x =>
                {
                    routeOptions = x;
                    // All generated URL's should append a trailing slash.
                    routeOptions.AppendTrailingSlash = true;
                    // All generated URL's should be lower-case.
                    routeOptions.LowercaseUrls = true;
                });

            // Add many MVC services to the services container.
            var mvcBuilder = services.AddMvc(
                mvcOptions =>
                {

                    var configurationSection = Configuration.GetSection(nameof(CacheProfileSettings));
                    var cacheProfileSettings = new CacheProfileSettings();
                    configurationSection.Bind(cacheProfileSettings);

                    foreach (var keyValuePair in cacheProfileSettings.CacheProfiles)
                    {
                        mvcOptions.CacheProfiles.Add(keyValuePair);
                    }

                    mvcOptions.Filters.Add(new RedirectToCanonicalUrlAttribute(routeOptions.AppendTrailingSlash, routeOptions.LowercaseUrls));
                });

            mvcBuilder.AddJsonOptions(
               x => x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.ConfigureAntiforgery(
                antiforgeryOptions =>
                {
                    antiforgeryOptions.CookieName = "f";
                    antiforgeryOptions.FormFieldName = "f";
                });

            if (HostingEnvironment.IsProduction())
            {
                mvcBuilder.AddPrecompiledRazorViews(typeof(Startup).Assembly);
            }

            services.AddSingleton<IHealthService, HealthService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IStorageService, CloudStorageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsProduction())
            {
                app.UseApplicationInsightsRequestTelemetry();
                app.UseApplicationInsightsExceptionTelemetry();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Add error handling middle-ware which handles all HTTP status codes from 400 to 599 by re-executing
                // the request pipeline for the following URL. '{0}' is the HTTP status code e.g. 404.
                app.UseStatusCodePagesWithReExecute("/error/{0}");

                // Returns a 500 Internal Server Error response when an unhandled exception occurs.
                //app.UseInternalServerErrorOnException();
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.Use((context, next) =>
             {
                 if (context.Request.Path.StartsWithSegments("/ping"))
                 {
                     return context.Response.WriteAsync("pong");
                 }
                 return next();
             });

            app.UseMvc();
        }
    }
}
