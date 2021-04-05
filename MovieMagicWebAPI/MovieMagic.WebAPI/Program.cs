using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MovieMagic.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogger();
            Log.Information("Application Started!");

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                // [INFO] Closing opened serilog session
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    // [INFO] Remove any server identification from header
                    .UseKestrel(options => options.AddServerHeader = false)
                    .UseSerilog();
                });

        public static void ConfigureLogger()
        {
            // [INFO] Adding Serilog Configuration
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
