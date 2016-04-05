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
using WebMarkupMin.AspNet5;


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

            services.ConfigureRouting(
                routeOptions =>
                {
                    routeOptions.AppendTrailingSlash = true;
                    routeOptions.LowercaseUrls = true;
                });


            /*

               <html whitespaceMinificationMode="Medium"
               removeHtmlComments="true"
               removeHtmlCommentsFromScriptsAndStyles="true"
               removeCdataSectionsFromScriptsAndStyles="true"
               useShortDoctype="true"
               useMetaCharsetTag="true"
               emptyTagRenderMode="NoSlash"
               removeOptionalEndTags="true"
               removeTagsWithoutContent="false"
               collapseBooleanAttributes="true"
               removeEmptyAttributes="true"
               attributeQuotesRemovalMode="Html5"
               removeRedundantAttributes="true" removeJsTypeAttributes="true"
               removeCssTypeAttributes="true" removeHttpProtocolFromAttributes="false"
               removeHttpsProtocolFromAttributes="false" removeJsProtocolFromAttributes="true"
               minifyEmbeddedCssCode="true" minifyInlineCssCode="true"
               minifyEmbeddedJsCode="true" minifyInlineJsCode="true" processableScriptTypeList=""
               minifyKnockoutBindingExpressions="false" minifyAngularBindingExpressions="false"
               customAngularDirectiveList="" />




             */


            // Add WebMarkupMin services to the services container.
            services.AddWebMarkupMin(options =>
            {
                //options.AllowMinificationInDevelopmentEnvironment = true;
               // options.AllowCompressionInDevelopmentEnvironment = true;
            }).AddHtmlMinification();

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

            app.UseWebMarkupMin();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
