using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Ordering.Infrastructure.Persistence.Configurations;

internal sealed class OrderGuaranteeConfiguration : IEntityTypeConfiguration<OrderGuarantee>
{
    public void Configure(EntityTypeBuilder<OrderGuarantee> builder)
    {
        builder.ToTable("order_guarantees", OrderingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new OrderGuaranteeId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.OrderPartId)
            .HasConversion<Guid?>(
                id => id.HasValue ? id.Value.Id : null,
                value => value.HasValue ? new OrderPartId(value.Value) : null)
            .IsRequired(false);

        builder.HasOne<OrderPart>()
            .WithMany()
            .HasForeignKey(x => x.OrderPartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.OrderWorkId)
            .HasConversion<Guid?>(
                id => id.HasValue ? id.Value.Id : null,
                value => value.HasValue ? new OrderWorkId(value.Value) : null)
            .IsRequired(false);

        builder.HasOne<OrderWork>()
            .WithMany()
            .HasForeignKey(x => x.OrderWorkId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.DateStart).IsRequired();
        builder.Property(x => x.DateEnd).IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(OrderGuarantee.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.Terms)
            .HasMaxLength(OrderGuarantee.MaxTermsLength)
            .IsRequired();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.OrderPartId);
        builder.HasIndex(x => x.OrderWorkId);
    }
}
