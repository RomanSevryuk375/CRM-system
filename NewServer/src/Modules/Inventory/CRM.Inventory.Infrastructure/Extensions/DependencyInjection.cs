using CRM.Inventory.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Inventory.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInventoryInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<InventoryDbContext>(configuration);

        return services;
    }
}
