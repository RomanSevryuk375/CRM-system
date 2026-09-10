using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("bills", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new BillId(value))
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasConversion(
                id => id.Id,
                value => new OrderId(value))
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired();

        builder.Property(x => x.ActualBillDate).IsRequired(false);

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.Status);

        builder.HasMany(x => x.PaymentNotes)
               .WithOne()
               .HasForeignKey(x => x.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
               .FindNavigation(nameof(Bill.PaymentNotes))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
