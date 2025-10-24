using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess;

public class SystemDbContext : DbContext
{
    public SystemDbContext(DbContextOptions<SystemDbContext> options) :
        base(options)
    {
    }

    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<CarEntity> Cars { get; set; }
    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<PaymentJournalEntity> PaymentJournals { get; set; }
    public DbSet<RepairHistoryEntity> repairHistories { get; set; }
    public DbSet<StatusEntity> statuses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SystemDbContext).Assembly);
    }
}
