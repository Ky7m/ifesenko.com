using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PersonalWebApp
{
    public static class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) => { options.AddServerHeader = false; });
    }
}