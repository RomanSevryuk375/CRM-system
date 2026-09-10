using CRM.Notifications.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Notifications.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddNotificationsInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<NotificationsDbContext>(configuration);

        return services;
    }
}
