using ifesenko.com.Infrastructure.Services.Implementation;
using ifesenko.com.Infrastructure.Services.Implementation.CloudStorageService;
using ifesenko.com.Infrastructure.Services.Implementation.HealthService;
using ifesenko.com.Infrastructure.Services.Interfaces;
using ifesenko.com.Infrastructure.Settings;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;

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

            //services.AddCaching();
            // services.AddTransient<IDistributedCache, RedisCache>();

            services.ConfigureRouting(
                routeOptions =>
                {
                    routeOptions.AppendTrailingSlash = true;
                    routeOptions.LowercaseUrls = true;
                });

            services.AddMvc();

            services.AddSingleton<IHealthService, HealthService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IStorageService, CloudStorageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/error/{0}");
            }

            if (env.IsProduction())
            {
                app.UseApplicationInsightsExceptionTelemetry();
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
