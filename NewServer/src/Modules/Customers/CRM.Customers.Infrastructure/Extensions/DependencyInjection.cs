using CRM.Customers.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Customers.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<CustomersDbContext>(configuration);

        return services;
    }
}
