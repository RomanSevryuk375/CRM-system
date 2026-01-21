using CRM_system_backend.Extensions;
using CRM_system_backend.Middlewares;
using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Extensions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.DataAccess;
using CRMSystem.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CRM_system_backend;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("SystemDbContext")!)
            .AddRedis(builder.Configuration.GetConnectionString("Redis")!); // I couldn't implement health check for S3
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Frontend", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .WithExposedHeaders("x-total-count");
            });
            options.AddPolicy("AllowAllOriginsDevelopment",
                policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .WithExposedHeaders("x-total-count");
                });
        });

        builder.Services.AddDbContext<SystemDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SystemDbContext)))
                   .UseSnakeCaseNamingConvention();
        });

        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            var connetion = builder.Configuration
                .GetConnectionString("Redis");

            redisOptions.Configuration = connetion;
        });

        builder.Services.AddScoped<IUserContext, UserContext>(); 
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IMyPasswordHasher, MyPasswordHasher>();
        builder.Services.AddScoped<IFileService, MinioFileService>();

        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddApiAuthentication(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Frontend");
        }
        else
        {
            app.UseCors("Frontend");
        }

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseCustomException();
        app.MapHealthChecks("/health");

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        
    }
}

public interface IApiMarker { }