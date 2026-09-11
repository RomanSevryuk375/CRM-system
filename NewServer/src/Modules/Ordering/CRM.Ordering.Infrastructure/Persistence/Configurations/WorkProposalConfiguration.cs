using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class WorkProposalConfiguration : IEntityTypeConfiguration<WorkProposal>
{
    public void Configure(EntityTypeBuilder<WorkProposal> builder)
    {
        builder.ToTable("work_proposals", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.JobId).IsRequired();
        builder.Property(x => x.WorkerId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Date).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.JobId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.Status);
    }
}
