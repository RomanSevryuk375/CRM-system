using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Infrastructure.Data;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.Interceptors;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CRM.Shared.Infrastructure.Extensions;

public static class AddModuleDatabase
{
    public static IServiceCollection AddMyModuleDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
    {
        string? connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<TDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();

            options.AddMyInterceptors(sp);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();

        services.AddScoped<OutboxMessageProcessorService<TDbContext>>();

        services.AddHostedService<DatabaseMigrationService<TDbContext>>();

        services.Decorate(
            typeof(INotificationHandler<>),
            typeof(IdempotentDomainEventHandler<,>).MakeGenericType(typeof(IDomainEvent), typeof(TDbContext))
        );

        return services;
    }
}

internal sealed class DatabaseMigrationService<TDbContext>(IServiceProvider serviceProvider)
    : IHostedService where TDbContext : DbContext
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
