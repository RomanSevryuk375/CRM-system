using CRMSystem.Business.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRM_system_backend.Extensions;

public static class ApiExtensions
{
    public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection(nameof(JwtOptions));
        var jwtOptions = jwtSection.Get<JwtOptions>();

        if (jwtOptions == null || string.IsNullOrEmpty(jwtOptions.SecretKey))
            throw new Exception("JWT configuration missing or invalid");

        services.Configure<JwtOptions>(jwtSection);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies["jwt"];
                    if (!string.IsNullOrEmpty(token))
                        context.Token = token;
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminUserPolicy", policy =>
            {
                policy.RequireClaim("userRoleId", "1", "2"); 
            });

            options.AddPolicy("AdminWorkerPolicy", options =>
            {
                options.RequireClaim("userRoleId", "1", "3");
            });

            options.AddPolicy("UniPolicy", options =>
            {
                options.RequireClaim("userRoleId", "1", "2", "3");
            });

            options.AddPolicy("UserPolicy", policy =>
            {
                policy.RequireClaim("userRoleId", "2");
            });

            options.AddPolicy("WorkerPolicy", policy =>
            {
                policy.RequireClaim("userRoleId", "3");
            });

            options.AddPolicy("AdminPolicy", policy =>
            {
                policy.RequireClaim("userRoleId", "1");
            });
        });
    }
}
