using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.ToTable("bills", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OrderId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.ActualBillDate).IsRequired(false);

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.Status);

        builder.HasMany(x => x.PaymentNotes)
               .WithOne()
               .HasForeignKey(x => x.BillId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.PaymentNotes)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
