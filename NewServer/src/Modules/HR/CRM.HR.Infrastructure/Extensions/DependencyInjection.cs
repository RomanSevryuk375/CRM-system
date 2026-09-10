using CRM.HR.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.HR.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddHrInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<HrDbContext>(configuration);

        return services;
    }
}
