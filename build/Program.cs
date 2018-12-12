using Cake.Frosting;

namespace Build
{
    public class Program : IFrostingStartup
    {
        public void Configure(ICakeServices services)
        {
            services.UseContext<Context>();
            services.UseLifetime<Lifetime>();
            services.UseWorkingDirectory("..");
        }

        public static int Main(string[] args) => new CakeHostBuilder()
            .WithArguments(args)
            .UseStartup<Program>()
            .Build()
            .Run();
    }
}