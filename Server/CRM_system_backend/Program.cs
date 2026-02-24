using CRM_system_backend.Extensions;
using CRM_system_backend.Middlewares;
using QuestPDF.Infrastructure;
using Serilog;

namespace CRM_system_backend;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);
        
            builder.Host.UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)); 
        
        QuestPDF.Settings.License = LicenseType.Community;
        
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
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

public interface IApiMarker { }