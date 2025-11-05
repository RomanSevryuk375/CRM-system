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

        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IUsedPartService, UsedPartService>();
        builder.Services.AddScoped<IUsedPartRepository, UsedPartRepository>();
        builder.Services.AddScoped<IWorkPropossalService, WorkPropossalService>();
        builder.Services.AddScoped<IWorkPropossalRepository,  WorkPropossalRepository>();
        builder.Services.AddScoped<IWorkService, WorkService>();
        builder.Services.AddScoped<IWorkRepository, WorkRepository>();
        builder.Services.AddScoped<IPaymentNoteService, PaymentNoteService>();
        builder.Services.AddScoped<IPaymentNoteRepository, PaymentNoteRepository>();
        builder.Services.AddScoped<IBillService, BillService>();
        builder.Services.AddScoped<IBillRepository,  BillRepository>();
        builder.Services.AddScoped<IStatusService, StatusService>();
        builder.Services.AddScoped<IStatusRepository, StatusRepository>();
        builder.Services.AddScoped<IRepairNoteService, RepairNoteService>();
        builder.Services.AddScoped<IRepairNoteRepositry, RepairNoteRepositry>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IWorkTypepService, WorkTypepService>();
        builder.Services.AddScoped<IWorkTypeRepository, WorkTypeRepository>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
        builder.Services.AddScoped<IWorkerService, WorkerService>();
        builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
        builder.Services.AddScoped<ITaxService, TaxService>();
        builder.Services.AddScoped<ITaxRepository, TaxRepository>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<ISpecializationService, SpecializationService>();
        builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
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
