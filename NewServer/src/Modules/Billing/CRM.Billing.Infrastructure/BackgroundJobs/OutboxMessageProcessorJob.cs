using CRM.Billing.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Results;
using CRM.Shared.Infrastructure.OutboxMessages;
using Quartz;

namespace CRM.Billing.Infrastructure.BackgroundJobs;

internal sealed class OutboxMessageProcessorJob(
    OutboxMessageProcessorService<BillingDbContext> service) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Result result = await service.ProcessAsync(context.CancellationToken);

        if (result.IsFailure)
        {
            throw new JobExecutionException(result.Error.Message);
        }
    }
}
