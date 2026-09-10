using CRM.HR.Domain.Entities;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace CRM.HR.Infrastructure.Persistence;

internal class HrDbContext(DbContextOptions<HrDbContext> options) : DbContext(options)
{
    public const string Schema = "hr";

    public DbSet<Worker> Workers => Set<Worker>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Absence> Absences => Set<Absence>();
    public DbSet<Shift> Shifts => Set<Shift>();
    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
