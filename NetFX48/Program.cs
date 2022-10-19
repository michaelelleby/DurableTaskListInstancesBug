using System;
using System.IO;
using Azure.Identity;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DurableTaskListInstances
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = ConfigureServiceCollection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddApplicationInsights();
            });
            var config = new JobHostConfiguration
            {
                DashboardConnectionString = ""
            };

            config.LoggerFactory = factory;

            config.UseTimers();
            config.UseCore();
            config.UseDurableTask(new DurableTaskExtension
            {
                HubName = "MyTaskHub",
            });

            var host = new JobHost(config);
            host.RunAndBlock();
        }

        private static IServiceCollection ConfigureServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHttpClient()
                .AddLogging()
                .AddTransient<Functions>();

            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();

            var configuration = configBuilder.Build();
            services.AddTransient<IConfiguration>(_ => configuration);

            return services;
        }
    }
}