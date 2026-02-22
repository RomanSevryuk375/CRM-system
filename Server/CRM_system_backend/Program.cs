using CRM_system_backend.Extensions;
using CRM_system_backend.Middlewares;
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
        builder.Host.UseSerilog(); 
        
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddCustomCors();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("Frontend");
        app.ApplyMigrations(); 

        app.UseCustomException(); 
        app.MapHealthChecks("/health");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        Log.Information("Starting web host");
        app.Run();
    }
}

public interface IApiMarker { }