using CRM_system_backend.Extensions;
using CRM_system_backend.Middlewares;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Buisnes.Cached;
using CRMSystem.Buisnes.Extensions;
using CRMSystem.Buisnes.Services;
using CRMSystem.DataAccess;
using CRMSystem.DataAccess.Repositories;
using CRMSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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
        builder.Services.AddOpenApi();

        builder.Services.AddDbContext<SystemDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(SystemDbContext)))
                   .UseSnakeCaseNamingConvention();
        });

        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
        builder.Services.AddScoped<IMyPasswordHasher, MyPasswordHasher>();

        builder.Services.AddScoped<IAbsenceRepository, AbsenceRepository>();
        builder.Services.AddScoped<IAbsenceTypeRepository, AbsenceTypeRepository>();
        builder.Services.AddScoped<IAcceptanceRepository, AcceptanceRepository>();
        builder.Services.AddScoped<IAcceptanceImgRepository, AcceptanceImgRepository>();
        builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        builder.Services.AddScoped<IAttachmentImgRepository, AttachmentImgRepository>();
        builder.Services.AddScoped<IBillRepository, BillRepository>();
        builder.Services.AddScoped<IBillStatusRepository, BillStatusRepository>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<ICarStatusRepository, CarStatusRepository>();
        builder.Services.AddScoped<IClientRepository, ClientsRepository>();
        builder.Services.AddScoped<IExpenseRespository, ExpenseRespository>();
        builder.Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
        builder.Services.AddScoped<IGuaranteeRepository, GuaranteeRepository>();
        builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
        builder.Services.AddScoped<INotificationStatusRepository, NotificationStatusRepository>();
        builder.Services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderPriorityRepository, OrderPriorityRepository>();
        builder.Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        builder.Services.AddScoped<IPartRepository, PartRepository>();
        builder.Services.AddScoped<IPartCategoryRepository, PartCategoryRepository>();
        builder.Services.AddScoped<IPartSetRepository, PartSetRepository>();
        builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        builder.Services.AddScoped<IPaymentNoteRepository, PaymentNoteRepository>();
        builder.Services.AddScoped<IPositionRepository, PositionRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
        builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
        builder.Services.AddScoped<ISkillRepository, SkillRepository>();
        builder.Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        builder.Services.AddScoped<IStorageCellRepository, StorageCellRepository>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<ISupplyRepository, SupplyRepository>();
        builder.Services.AddScoped<ISupplySetRepository, SupplySetRepository>();
        builder.Services.AddScoped<ITaxRepository, TaxRepository>();
        builder.Services.AddScoped<ITaxTypeRepository, TaxTypeRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IWorkRepository, WorkRepository>();
        builder.Services.AddScoped<IWorkInOrderRepository, WorkInOrderRepository>();
        builder.Services.AddScoped<IWorkInOrderStatusRepository, WorkInOrderStatusRepository>();
        builder.Services.AddScoped<IWorkerRepository, WorkerRepository>();
        builder.Services.AddScoped<IWorkProposalRepository, WorkProposalRepository>(); 
        builder.Services.AddScoped<IWorkProposalStatusRepository, WorkProposalStatusRepository>();

        builder.Services.AddScoped<IAbsenceService, AbsenceService>();
        builder.Services.AddScoped<IAcceptanceService, AcceptanceService>();
        builder.Services.AddScoped<IAcceptanceImgService, AcceptanceImgService>();
        builder.Services.AddScoped<IAttachmentService, AttachmentService>();
        builder.Services.AddScoped<IAttachmentImgService, AttachmentImgService>();
        builder.Services.AddScoped<IBillService, BillService>();
        builder.Services.AddScoped<IClientService, ClientService>(); 
        builder.Services.AddScoped<IExpenseService, ExpenseService>();
        builder.Services.AddScoped<IGuaranteeService, GuaranteeService>();
        builder.Services.AddScoped<INotificationService, NotificationService>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPartService, PartService>();
        builder.Services.AddScoped<IPartSetService, PartSetService>();
        builder.Services.AddScoped<IPaymentNoteService, PaymentNoteService>();
        builder.Services.AddScoped<IPositionSrevice, PositionSevice>(); 
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IScheduleService, ScheduleService>();
        builder.Services.AddScoped<IShiftService, ShiftService>();
        builder.Services.AddScoped<ISkillService, SkillService>();
        builder.Services.AddScoped<IStorageCellService, StorageCellService>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
        builder.Services.AddScoped<ISupplySetService, SupplySetService>();
        builder.Services.AddScoped<ISupplySetService, SupplySetService>();
        builder.Services.AddScoped<ITaxService, TaxService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IWorkService, WorkService>();
        builder.Services.AddScoped<IWorkInOrderService, WorkInOrderService>();
        builder.Services.AddScoped<IWorkerService, WorkerService>();
        builder.Services.AddScoped<IWorkPropossalService, WorkPropossalService>(); 

        builder.Services.AddScoped<AbsenceTypeService>();
        builder.Services.AddScoped<IAbsenceTypeService>(provider =>
            new CachedAbsenceTypeService(
                provider.GetRequiredService<AbsenceTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<AbsenceTypeService>>() 
            ));

        builder.Services.AddScoped<BillStatusService>();
        builder.Services.AddScoped<IBillStatusService>(provider =>
            new CachedBillStatusService(
                provider.GetRequiredService<BillStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedBillStatusService>>()
            ));

        builder.Services.AddScoped<CarService>();
        builder.Services.AddScoped<ICarService>(provider =>
            new CachedCarService(
                provider.GetRequiredService<CarService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedCarService>>()
            ));

        builder.Services.AddScoped<CarStatusService>();
        builder.Services.AddScoped<ICarStatusService>(provider =>
            new CachedCarStatusService(
                provider.GetRequiredService<CarStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedCarStatusService>>()
            ));

        builder.Services.AddScoped<ClientService>();
        builder.Services.AddScoped<IClientService>(provider =>
            new CachedClientService(
                provider.GetRequiredService<ClientService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedClientService>>()
            ));

        builder.Services.AddScoped<ExpenseTypeService>();
        builder.Services.AddScoped<IExpenseTypeService>(provider =>
            new CachedExpenseTypeService(
                provider.GetRequiredService<ExpenseTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedExpenseTypeService>>()
            ));

        builder.Services.AddScoped<NotificationStatusService>();
        builder.Services.AddScoped<INotificationStatusService>(provider =>
            new CachedNotificationStatusService( 
                provider.GetRequiredService<NotificationStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedNotificationStatusService>>()
            ));

        builder.Services.AddScoped<NotificationTypeService>();
        builder.Services.AddScoped<INotificationTypeService>(provider =>
            new CachedNotificationTypeService(
                provider.GetRequiredService<NotificationTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedNotificationTypeService>>()
            ));

        builder.Services.AddScoped<OrderPriorityService>();
        builder.Services.AddScoped<IOrderPriorityService>(provider =>
            new CachedOrderPriorityService(
                provider.GetRequiredService<OrderPriorityService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedOrderPriorityService>>()
            ));

        builder.Services.AddScoped<PartCategoryService>();
        builder.Services.AddScoped<IPartCategoryService>(provider =>
            new CachedPartCategoryService(
                provider.GetRequiredService<PartCategoryService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPartCategoryService>>()
            ));

        builder.Services.AddScoped<PartSetService>();
        builder.Services.AddScoped<IPartSetService>(provider =>
            new CachedPartSetService(
                provider.GetRequiredService<PartSetService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPartSetService>>()
            ));

        builder.Services.AddScoped<PaymentMethodService>();
        builder.Services.AddScoped<IPaymentMethodService>(provider =>
            new CachedPaymentMethodService(
                provider.GetRequiredService<PaymentMethodService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPaymentMethodService>>()
            ));

        builder.Services.AddScoped<RoleService>();
        builder.Services.AddScoped<IRoleService>(provider =>
            new CacheRoleService(
                provider.GetRequiredService<RoleService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CacheRoleService>>()
            ));

        builder.Services.AddScoped<SpecializationService>();
        builder.Services.AddScoped<ISpecializationService>(provider =>
            new CachedSpecializationService(
                provider.GetRequiredService<SpecializationService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedSpecializationService>>()
            ));

        builder.Services.AddScoped<TaxTypeService>();
        builder.Services.AddScoped<ITaxTypeService>(provider =>
            new CachedTaxTypeService(
                provider.GetRequiredService<TaxTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedTaxTypeService>>()
            ));

        builder.Services.AddScoped<WorkInOrderStatusService>();
        builder.Services.AddScoped<IWorkInOrderStatusService>(provider =>
            new CachedWorkInOrderStatusService(
                provider.GetRequiredService<WorkInOrderStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedWorkInOrderStatusService>>()
            ));

        builder.Services.AddScoped<WorkProposalStatusService>();
        builder.Services.AddScoped<IWorkProposalStatusService>(provider =>
            new CachedWorkProposalStatusService(
                provider.GetRequiredService<WorkProposalStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedWorkProposalStatusService>>()
            ));


        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            var connetion = builder.Configuration
                .GetConnectionString("Redis");

            redisOptions.Configuration = connetion;
        });
        builder.Services.AddApiAuthentication(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
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
            dbContext.Database.EnsureCreated();
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomException();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
