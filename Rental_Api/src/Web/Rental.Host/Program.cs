using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Gelf.Extensions.Logging;
using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Rental.Host
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((builderContext, config) =>
                    {
                        var env = builderContext.HostingEnvironment;
                        config.AddJsonFile("./Configs/appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"./Configs/appsettings.{env.EnvironmentName}.json", optional: true,
                                reloadOnChange: true)
                            .AddEnvironmentVariables();
                    }).UseStartup<Startup>();
                });

    }
}
