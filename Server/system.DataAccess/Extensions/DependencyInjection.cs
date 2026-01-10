using CRMSystem.Core.Abstractions;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRMSystem.DataAccess.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection Services)
    {

        Services.AddScoped<IAbsenceRepository, AbsenceRepository>();
        Services.AddScoped<IAbsenceTypeRepository, AbsenceTypeRepository>();
        Services.AddScoped<IAcceptanceRepository, AcceptanceRepository>();
        Services.AddScoped<IAcceptanceImgRepository, AcceptanceImgRepository>();
        Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        Services.AddScoped<IAttachmentImgRepository, AttachmentImgRepository>();
        Services.AddScoped<IBillRepository, BillRepository>();
        Services.AddScoped<IBillStatusRepository, BillStatusRepository>();
        Services.AddScoped<ICarRepository, CarRepository>();
        Services.AddScoped<ICarStatusRepository, CarStatusRepository>();
        Services.AddScoped<IClientRepository, ClientsRepository>();
        Services.AddScoped<IExpenseRepository, ExpenseRepository>();
        Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
        Services.AddScoped<IGuaranteeRepository, GuaranteeRepository>();
        Services.AddScoped<INotificationRepository, NotificationRepository>();
        Services.AddScoped<INotificationStatusRepository, NotificationStatusRepository>();
        Services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
        Services.AddScoped<IOrderRepository, OrderRepository>();
        Services.AddScoped<IOrderPriorityRepository, OrderPriorityRepository>();
        Services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        Services.AddScoped<IPartRepository, PartRepository>();
        Services.AddScoped<IPartCategoryRepository, PartCategoryRepository>();
        Services.AddScoped<IPartSetRepository, PartSetRepository>();
        Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        Services.AddScoped<IPaymentNoteRepository, PaymentNoteRepository>();
        Services.AddScoped<IPositionRepository, PositionRepository>();
        Services.AddScoped<IRoleRepository, RoleRepository>();
        Services.AddScoped<IScheduleRepository, ScheduleRepository>();
        Services.AddScoped<IShiftRepository, ShiftRepository>();
        Services.AddScoped<ISkillRepository, SkillRepository>();
        Services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        Services.AddScoped<IStorageCellRepository, StorageCellRepository>();
        Services.AddScoped<ISupplierRepository, SupplierRepository>();
        Services.AddScoped<ISupplyRepository, SupplyRepository>();
        Services.AddScoped<ISupplySetRepository, SupplySetRepository>();
        Services.AddScoped<ITaxRepository, TaxRepository>();
        Services.AddScoped<ITaxTypeRepository, TaxTypeRepository>();
        Services.AddScoped<IUserRepository, UserRepository>();
        Services.AddScoped<IWorkRepository, WorkRepository>();
        Services.AddScoped<IWorkInOrderRepository, WorkInOrderRepository>();
        Services.AddScoped<IWorkInOrderStatusRepository, WorkInOrderStatusRepository>();
        Services.AddScoped<IWorkerRepository, WorkerRepository>();
        Services.AddScoped<IWorkProposalRepository, WorkProposalRepository>();
        Services.AddScoped<IWorkProposalStatusRepository, WorkProposalStatusRepository>();

        Services.AddScoped<IUnitOfWork, UnitOfWork>();

        return Services;
    }
}
