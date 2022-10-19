using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System.Threading;
using System.Threading.Tasks;

namespace DurableTaskListInstances
{
    public class Functions
    {
        public async Task TimerStart([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, [DurableClient] IDurableOrchestrationClient client)
        {
            await client.ListInstancesAsync(new OrchestrationStatusQueryCondition(), CancellationToken.None);
        }
    }
}
