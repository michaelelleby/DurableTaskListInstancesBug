using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace NetFX48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
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
    }
}