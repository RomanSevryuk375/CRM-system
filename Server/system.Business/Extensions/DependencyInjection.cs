using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Cached;
using CRMSystem.Business.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection Services)
    {
        Services.AddScoped<IAbsenceService, AbsenceService>();
        Services.AddScoped<IAcceptanceService, AcceptanceService>();
        Services.AddScoped<IAcceptanceImgService, AcceptanceImgService>();
        Services.AddScoped<IAttachmentService, AttachmentService>();
        Services.AddScoped<IAttachmentImgService, AttachmentImgService>();
        Services.AddScoped<IBillService, BillService>();
        Services.AddScoped<IClientService, ClientService>();
        Services.AddScoped<IExpenseService, ExpenseService>();
        Services.AddScoped<IGuaranteeService, GuaranteeService>();
        Services.AddScoped<INotificationService, NotificationService>();
        Services.AddScoped<IOrderService, OrderService>();
        Services.AddScoped<IPartService, PartService>();
        Services.AddScoped<IPartSetService, PartSetService>();
        Services.AddScoped<IPaymentNoteService, PaymentNoteService>();
        Services.AddScoped<IPositionService, PositionService>();
        Services.AddScoped<IRoleService, RoleService>();
        Services.AddScoped<IScheduleService, ScheduleService>();
        Services.AddScoped<IShiftService, ShiftService>();
        Services.AddScoped<ISkillService, SkillService>();
        Services.AddScoped<IStorageCellService, StorageCellService>();
        Services.AddScoped<ISupplierService, SupplierService>();
        Services.AddScoped<ISupplyService, SupplyService>();
        Services.AddScoped<ISupplySetService, SupplySetService>();
        Services.AddScoped<ITaxService, TaxService>();
        Services.AddScoped<IUserService, UserService>();
        Services.AddScoped<IWorkService, WorkService>();
        Services.AddScoped<IWorkInOrderService, WorkInOrderService>();
        Services.AddScoped<IWorkerService, WorkerService>();
        Services.AddScoped<IWorkProposalService, WorkProposalService>();

        Services.AddScoped<AbsenceTypeService>();
        Services.AddScoped<IAbsenceTypeService>(provider =>
            new CachedAbsenceTypeService(
                provider.GetRequiredService<AbsenceTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<AbsenceTypeService>>()
            ));

        Services.AddScoped<BillStatusService>();
        Services.AddScoped<IBillStatusService>(provider =>
            new CachedBillStatusService(
                provider.GetRequiredService<BillStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedBillStatusService>>()
            ));

        Services.AddScoped<CarService>();
        Services.AddScoped<ICarService>(provider =>
            new CachedCarService(
                provider.GetRequiredService<CarService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedCarService>>()
            ));

        Services.AddScoped<CarStatusService>();
        Services.AddScoped<ICarStatusService>(provider =>
            new CachedCarStatusService(
                provider.GetRequiredService<CarStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedCarStatusService>>()
            ));

        Services.AddScoped<ClientService>();
        Services.AddScoped<IClientService>(provider =>
            new CachedClientService(
                provider.GetRequiredService<ClientService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedClientService>>()
            ));

        Services.AddScoped<ExpenseTypeService>();
        Services.AddScoped<IExpenseTypeService>(provider =>
            new CachedExpenseTypeService(
                provider.GetRequiredService<ExpenseTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedExpenseTypeService>>()
            ));

        Services.AddScoped<NotificationStatusService>();
        Services.AddScoped<INotificationStatusService>(provider =>
            new CachedNotificationStatusService(
                provider.GetRequiredService<NotificationStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedNotificationStatusService>>()
            ));

        Services.AddScoped<NotificationTypeService>();
        Services.AddScoped<INotificationTypeService>(provider =>
            new CachedNotificationTypeService(
                provider.GetRequiredService<NotificationTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedNotificationTypeService>>()
            ));

        Services.AddScoped<OrderPriorityService>();
        Services.AddScoped<IOrderPriorityService>(provider =>
            new CachedOrderPriorityService(
                provider.GetRequiredService<OrderPriorityService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedOrderPriorityService>>()
            ));

        Services.AddScoped<PartCategoryService>();
        Services.AddScoped<IPartCategoryService>(provider =>
            new CachedPartCategoryService(
                provider.GetRequiredService<PartCategoryService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPartCategoryService>>()
            ));

        Services.AddScoped<PartSetService>();
        Services.AddScoped<IPartSetService>(provider =>
            new CachedPartSetService(
                provider.GetRequiredService<PartSetService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPartSetService>>()
            ));

        Services.AddScoped<PaymentMethodService>();
        Services.AddScoped<IPaymentMethodService>(provider =>
            new CachedPaymentMethodService(
                provider.GetRequiredService<PaymentMethodService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedPaymentMethodService>>()
            ));

        Services.AddScoped<RoleService>();
        Services.AddScoped<IRoleService>(provider =>
            new CacheRoleService(
                provider.GetRequiredService<RoleService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CacheRoleService>>()
            ));

        Services.AddScoped<SpecializationService>();
        Services.AddScoped<ISpecializationService>(provider =>
            new CachedSpecializationService(
                provider.GetRequiredService<SpecializationService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedSpecializationService>>()
            ));

        Services.AddScoped<TaxTypeService>();
        Services.AddScoped<ITaxTypeService>(provider =>
            new CachedTaxTypeService(
                provider.GetRequiredService<TaxTypeService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedTaxTypeService>>()
            ));

        Services.AddScoped<WorkInOrderStatusService>();
        Services.AddScoped<IWorkInOrderStatusService>(provider =>
            new CachedWorkInOrderStatusService(
                provider.GetRequiredService<WorkInOrderStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedWorkInOrderStatusService>>()
            ));

        Services.AddScoped<WorkProposalStatusService>();
        Services.AddScoped<IWorkProposalStatusService>(provider =>
            new CachedWorkProposalStatusService(
                provider.GetRequiredService<WorkProposalStatusService>(),
                provider.GetRequiredService<IDistributedCache>(),
                provider.GetRequiredService<ILogger<CachedWorkProposalStatusService>>()
            ));

        return Services;
    }
}
