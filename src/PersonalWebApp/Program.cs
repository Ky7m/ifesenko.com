﻿using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PersonalWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
               .UseKestrel()
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseIISIntegration()
               .UseApplicationInsights()
               .UseAzureAppServices()
               .UseStartup<Startup>()
               .Build();

            host.Run();
        }
    }
}


