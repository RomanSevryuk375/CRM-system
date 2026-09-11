using CRM.Ordering.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderWorkConfiguration : IEntityTypeConfiguration<OrderWork>
{
    public void Configure(EntityTypeBuilder<OrderWork> builder)
    {
        builder.ToTable("order_works", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();
        builder.Property(x => x.JobId).IsRequired();
        builder.Property(x => x.WorkerId).IsRequired(false);
        builder.Property(x => x.EstimatedHours).IsRequired();
        builder.Property(x => x.TimeSpent).IsRequired(false);
        builder.Property(x => x.IsProposed).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.HourlyRate).IsRequired(false);
        builder.Property(x => x.FixedPrice).IsRequired(false);
        builder.Property(x => x.TotalCost).IsRequired(false);

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.JobId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.Status);
    }
}
