using CRM.IAM.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using CRM.Shared.Infrastructure.Data.InboxMessages;
using CRM.Shared.Infrastructure.Data.OutboxMessages;
using Microsoft.EntityFrameworkCore;

namespace CRM.IAM.Infrastructure.Persistence;

internal class IamDbContext(DbContextOptions<IamDbContext> options) : DbContext(options)
{
    public const string Schema = "iam";

    public DbSet<User> Users => Set<User>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.AddSharedValueConverters();
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IamDbContext).Assembly);

        modelBuilder.Entity<OutboxMessage>().ConfigureOutboxMessage(Schema);
        modelBuilder.Entity<InboxMessage>().ConfigureInboxMessage(Schema);

        base.OnModelCreating(modelBuilder);
    }
}
