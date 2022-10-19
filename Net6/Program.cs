using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Hosting;

namespace Net6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebJobs(config =>
                {
                    config.AddAzureStorageCoreServices();
                    config.AddTimers();
                    config.AddDurableTask(options => { options.HubName = "MyTaskHub"; });
                })
                .UseConsoleLifetime();

            var host = hostBuilder.Build();

            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}