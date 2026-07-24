using CRM.Billing.Application.Extensions;
using CRM.Billing.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Billing.Api;

public static class BillingModule
{
    public static IServiceCollection AddBillingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBillingInfrastructure(configuration);
        services.AddBillingApplication();

        return services;
    }
}