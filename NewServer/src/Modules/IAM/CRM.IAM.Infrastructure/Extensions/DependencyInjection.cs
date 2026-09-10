using CRM.IAM.Infrastructure.Persistence;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.IAM.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddIamInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMyModuleDatabase<IamDbContext>(configuration);

        return services;
    }
}
