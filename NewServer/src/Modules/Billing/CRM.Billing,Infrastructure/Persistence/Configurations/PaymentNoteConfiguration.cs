using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class PaymentNoteConfiguration : IEntityTypeConfiguration<PaymentNote>
{
    public void Configure(EntityTypeBuilder<PaymentNote> builder)
    {
        builder.ToTable("payment_notes", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new PaymentNoteId(value))
            .IsRequired();

        builder.Property(x => x.BillId)
            .HasConversion(
                id => id.Id,
                value => new BillId(value))
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Method)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Date).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.BillId);
        builder.HasIndex(x => x.Method);
    }
}
