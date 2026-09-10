using CRM.Billing.Domain.Interfaces;
using CRM.Billing.Infrastructure.BackgroundJobs;
using CRM.Billing.Infrastructure.Persistence;
using CRM.Billing.Infrastructure.Persistence.Repositories;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace CRM.Billing.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBillingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddRepositories(configuration)
                       .AddQuartzJobs();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMyModuleDatabase<BillingDbContext>(configuration);

        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IPriceListRepository, PriceListRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();

        return services;
    }

    public static IServiceCollection AddQuartzJobs(this IServiceCollection services)
    {
        services.AddQuartz(opts =>
        {
            JobKey outboxJobKey = new(nameof(OutboxMessageProcessorJob));
            opts.AddJob<OutboxMessageProcessorJob>(jobOpts => jobOpts.WithIdentity(outboxJobKey));
            opts.AddTrigger(triggerOpts => triggerOpts
                .ForJob(outboxJobKey)
                .WithIdentity($"{outboxJobKey}-trigger")
                .WithSimpleSchedule(x =>
                {
                    const int SecondsInterval = 60;
                    x.WithIntervalInSeconds(SecondsInterval).RepeatForever();
                }));
        });

        services.AddQuartzHostedService(hostOptions
            => hostOptions.WaitForJobsToComplete = true);

        return services;
    }
}
