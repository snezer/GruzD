using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace GruzD.Web.Backend {
    public class Program {
        public static void Main(string[] args)
        {
            // TODO: ƒобавить параметры дл€ выбора окружени€ хостинга
            //Microsoft.AspNetCore.Hosting.WindowsServices.WebHostWindowsServiceExtensions.
            CreateHostBuilder(args)
                .Build()
                .Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var environment =
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); // TODO: Check Windows-specific

            var config = new ConfigurationBuilder()
                //.SetBasePath(Directory.Get())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddEnvironmentVariables().Build();

            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hosting, configuration) =>
                {
                    configuration.
                        ReadFrom.Configuration(config, "Serilog");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls(config["hostUrl"]);
                })
                .UseWindowsService()
                .UseSystemd();
        }
    }
}
