using ApiGateway;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using System;
using System.IO;

namespace CBCS.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    // config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                    //config.AddJsonFile($"ocelot.json", true, true);

                })

                // .UseSerilog(SeriLogger.Configure)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                        //.AddOcelot($"Configs\\{hostingContext.HostingEnvironment.EnvironmentName}", hostingContext.HostingEnvironment)
                        .AddOcelot(Path.Combine("Configs", hostingContext.HostingEnvironment.EnvironmentName), hostingContext.HostingEnvironment)
                        .AddEnvironmentVariables();
                    });
                });
    }
}
