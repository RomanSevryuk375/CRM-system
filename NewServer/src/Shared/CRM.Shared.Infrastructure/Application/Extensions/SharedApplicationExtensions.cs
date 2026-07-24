using CRM.Shared.Infrastructure.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Shared.Infrastructure.Application.Extensions;

public static class SharedApplicationExtensions
{
    public static IServiceCollection AddGlobalSharedApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        return services;
    }
}