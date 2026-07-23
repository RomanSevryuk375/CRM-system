using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Shared.Infrastructure.Extensions;

public static class SharedInfrastructureExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.RegisterMyInterceptors();

        return services;
    }
}