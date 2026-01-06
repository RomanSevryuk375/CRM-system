// Ignore Spelling: Imgs

using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess;

public class SystemDbContext : DbContext
{
    public SystemDbContext(DbContextOptions<SystemDbContext> options) :
        base(options)
    {
    }

    public DbSet<AbsenceEntity> Absences { get; set; }
    public DbSet<AbsenceTypeEntity> AbsenceTypes { get; set; }
    public DbSet<AcceptanceEntity> Acceptances { get; set; }
    public DbSet<AcceptanceImgEntity> AcceptanceImgs { get; set; }
    public DbSet<AttachmentEntity> Attachments { get; set; }
    public DbSet<AttachmentImgEntity> AttachmentImgs { get; set; }
    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<BillStatusEntity> BillStatuses { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<CarStatusEntity> CarStatuses { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }
    public DbSet<ExpenseTypeEntity> ExpenseTypes { get; set; }
    public DbSet<GuaranteeEntity> Guarantees { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }
    public DbSet<NotificationStatusEntity> NotificationsStatuses { get; set; }
    public DbSet<NotificationTypeEntity> NotificationTypes { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderStatusEntity> OrderStatuses { get; set; }
    public DbSet<OrderPriorityEntity> OrderPriorities { get; set; }
    public DbSet<PartCategoryEntity> PartCategories { get; set; }
    public DbSet<PartEntity> Parts { get; set; }
    public DbSet<PartSetEntity> PartSets { get; set; }
    public DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    public DbSet<PaymentNoteEntity> PaymentNotes { get; set; }
    public DbSet<PositionEntity> Positions { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<ScheduleEntity> Schedules { get; set; }
    public DbSet<ShiftEntity> Shifts { get; set; }
    public DbSet<SkillEntity> Skills { get; set; }
    public DbSet<SpecializationEntity> Specializations { get; set; }
    public DbSet<StorageCellEntity> StorageCells { get; set; }
    public DbSet<SupplierEntity> Suppliers { get; set; }
    public DbSet<SupplyEntity> Supplies { get; set; }
    public DbSet<SupplySetEntity> SupplySets { get; set; }
    public DbSet<TaxEntity> Taxes { get; set; }
    public DbSet<TaxTypeEntity> TaxTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WorkEntity> Works { get; set; }
    public DbSet<WorkerEntity> Workers { get; set; }
    public DbSet<WorkInOrderEntity> WorksInOrder { get; set; }
    public DbSet<WorkInOrderStatusEntity> WorkInOrderStatuses { get; set; }
    public DbSet<WorkProposalEntity> WorkProposals { get; set; }
    public DbSet<WorkProposalStatusEntity> WorkProposalStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemDbContext).Assembly);
    }
}
