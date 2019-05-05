using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UnderstandingDI
{
    class Program
    {
        public static string ConnectionString;

        static void Main(string[] args)
        {
            var services = ConfigureServices();

            // If using multiple 'Worker' classes
            // This can be wrapped inside a switch
            services.AddSingleton<Worker>();

            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation("Starting Application");

            using (var scope = serviceProvider.CreateScope())
            {
                var worker = serviceProvider.GetService<Worker>();
                worker.Run();
            }

            logger.LogInformation("Closing Application");
        }

        private static IServiceCollection ConfigureServices()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Settings>();

            var configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<UnderstandingDIContext>(options => options.UseSqlite(ConnectionString))
                .AddLogging(b => b
                    .AddConsole());

            return services;
        }
    }
}
