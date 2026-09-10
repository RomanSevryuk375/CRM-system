using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Ordering.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<OrderingDbContext>(configuration);

        return services;
    }
}
