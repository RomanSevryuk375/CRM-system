using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess;

public class SystemDbContext : DbContext
{
    public SystemDbContext(DbContextOptions<SystemDbContext> options) :
        base(options)
    {
    }

    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<ExpenseEntity> Expenses { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<PaymentJournalEntity> PaymentJournals { get; set; }
    public DbSet<ProposedPartEntity> ProposedParts { get; set; }
    public DbSet<RepairHistoryEntity> RepairHistories { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<SpecializationEntity> Specializations { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<SupplierEntity> Suppliers { get; set; }
    public DbSet<TaxEntity> Taxes { get; set; }
    public DbSet<UsedPartEntity> UsedParts { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<WorkEntity> Works { get; set; }
    public DbSet<WorkerEntity> Workers { get; set; }
    public DbSet<WorkProposalEntity> WorkProposals { get; set; }
    public DbSet<WorkTypeEntity> WorkTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemDbContext).Assembly);
    }
}
