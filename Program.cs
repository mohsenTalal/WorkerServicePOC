using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using WorkerServicePOC.ScopeProcess;

using Serilog;

namespace WorkerServicePOC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Users\a.aenzi\Desktop\WorkerServicePOC\Logs\log.txt")
                .CreateLogger();
            CreateHostBuilder(args).Build().Run();
            try
            {
                Log.Information("Starting up the services");
            }
            catch (Exception ex)
            {
                Log.Fatal("There was a problem stating the services");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                })
        .UseSerilog();
    }
}