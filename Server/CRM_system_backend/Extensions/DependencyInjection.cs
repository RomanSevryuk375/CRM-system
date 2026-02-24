using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.DataAccess;
using CRMSystem.DataAccess.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

namespace CRM_system_backend.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Base services
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOpenApi();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));

        // Controllers & Validators
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });
        
        services.AddFluentValidationAutoValidation();

        // DB & Cache
        services.AddDbContext<SystemDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(SystemDbContext)))
                .UseSnakeCaseNamingConvention();
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        
        services.AddRepositories();
        services.AddServices(); 
        services.AddApiAuthentication(configuration);

        // Infrastructure
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IMyPasswordHasher, MyPasswordHasher>();
        services.AddScoped<IFileService, MinioFileService>();

        // HealthChecks
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("SystemDbContext")!)
            .AddRedis(configuration.GetConnectionString("Redis")!);

        // Data Protection
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"))
            .SetApplicationName("AUTOService");

        return services;
    }
}