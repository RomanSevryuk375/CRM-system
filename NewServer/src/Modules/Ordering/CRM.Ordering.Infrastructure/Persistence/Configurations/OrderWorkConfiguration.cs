using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderWorkConfiguration : IEntityTypeConfiguration<OrderWork>
{
    public void Configure(EntityTypeBuilder<OrderWork> builder)
    {
        builder.ToTable("order_works", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new OrderWorkId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.JobId)
            .HasConversion(
                id => id.Id,
                value => new JobId(value))
            .IsRequired();

        builder.Property(x => x.WorkerId)
            .HasConversion<Guid?>(
                id => id.HasValue ? id.Value.Id : null,
                value => value.HasValue ? new WorkerId(value.Value) : null)
            .IsRequired(false);

        builder.Property(x => x.EstimatedHours)
            .HasConversion(
                vo => vo.Value,
                dbVal => StandardHours.Create(dbVal).Value)
            .HasPrecision(StandardHours.Precision, StandardHours.Scale)
            .IsRequired();

        builder.Property(x => x.TimeSpent)
            .HasConversion<decimal?>(
                vo => vo != null ? vo.Value : null,
                dbVal => dbVal.HasValue ? StandardHours.Create(dbVal.Value).Value : null)
            .HasPrecision(StandardHours.Precision, StandardHours.Scale)
            .IsRequired(false);

        builder.Property(x => x.IsProposed).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.HourlyRate)
            .HasConversion<decimal?>(
                vo => vo != null ? vo.Value : null,
                dbVal => dbVal.HasValue ? Money.Create(dbVal.Value).Value : null)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired(false);

        builder.Property(x => x.FixedPrice)
            .HasConversion<decimal?>(
                vo => vo != null ? vo.Value : null,
                dbVal => dbVal.HasValue ? Money.Create(dbVal.Value).Value : null)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired(false);

        builder.Property(x => x.TotalCost)
            .HasConversion<decimal?>(
                vo => vo != null ? vo.Value : null,
                dbVal => dbVal.HasValue ? Money.Create(dbVal.Value).Value : null)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired(false);

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.JobId);
        builder.HasIndex(x => x.WorkerId);
        builder.HasIndex(x => x.Status);
    }
}
