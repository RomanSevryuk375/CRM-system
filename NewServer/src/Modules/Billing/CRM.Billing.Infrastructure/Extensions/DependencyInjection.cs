using CRM.Billing.Domain.Interfaces;
using CRM.Billing.Infrastructure.BackgroundJobs;
using CRM.Billing.Infrastructure.Factories;
using CRM.Billing.Infrastructure.Persistence;
using CRM.Billing.Infrastructure.Persistence.Repositories;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure;
using CRM.Shared.Infrastructure.Interceptors;
using CRM.Shared.Infrastructure.OutboxMessages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace CRM.Billing.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddRepositories(configuration)
                       .AddQuartzJobs();
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOutboxRepository<BillingDbContext>, OutboxRepository<BillingDbContext>>();
        services.AddScoped<OutboxMessageProcessorService<BillingDbContext>>();
        services.AddScoped<IBillRepository, BillRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IPriceListRepository, PriceListRepository>();
        services.AddScoped<ITaxRepository, TaxRepository>();

        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

        string? connectionString = configuration.GetConnectionString(nameof(BillingDbContext));
        services.AddDbContext<BillingDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention();

            options.AddMyInterceptors(sp);
        });
        services.AddHealthChecks().AddNpgSql(connectionString!);

        services.AddScoped<IUnitOfWork, UnitOfWork<BillingDbContext>>();
        services.AddHostedService<DatabaseMigrationService>();

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

internal sealed class DatabaseMigrationService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        BillingDbContext context = scope.ServiceProvider.GetRequiredService<BillingDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
