using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Shared.Infrastructure.Interceptors;

public static class AddInterceptorsExtensions
{
    public static IServiceCollection RegisterMyInterceptors(this IServiceCollection services)
    {
        services.AddScoped<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddScoped<AuditInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();

        return services;
    }

    public static DbContextOptionsBuilder AddMyInterceptors(
        this DbContextOptionsBuilder optionsBuilder,
        IServiceProvider serviceProvider)
    {
        ConvertDomainEventsToOutboxMessagesInterceptor domainEventInterceptor =
            serviceProvider.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();

        AuditInterceptor auditInterceptor =
            serviceProvider.GetRequiredService<AuditInterceptor>();

        SoftDeleteInterceptor softDeleteInterceptor =
            serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

        return optionsBuilder.AddInterceptors(domainEventInterceptor, auditInterceptor, softDeleteInterceptor);
    }
}
