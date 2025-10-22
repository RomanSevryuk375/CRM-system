using CRM_system_backend.Extensions;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.DataAccess;
using CRMSystem.DataAccess.Repositories;
using CRMSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CRM_system_backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<SystemDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SystemDbContext)));
        });

        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IMyPasswordHasher, MyPasswordHasher>();
        builder.Services.AddScoped<IJwtProvider, JwtProvider>();

        builder.Services.AddApiAuthentication(builder.Configuration);

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SystemDbContext>();
            dbContext.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
