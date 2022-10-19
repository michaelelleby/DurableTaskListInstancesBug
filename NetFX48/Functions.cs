using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace NetFX48
{
    public class Functions
    {
        public async Task TimerStart([TimerTrigger("*/5 * * * * *")] TimerInfo myTimer, [DurableClient] IDurableOrchestrationClient client)
        {
            await client.ListInstancesAsync(new OrchestrationStatusQueryCondition(), CancellationToken.None);
        }
    }
}