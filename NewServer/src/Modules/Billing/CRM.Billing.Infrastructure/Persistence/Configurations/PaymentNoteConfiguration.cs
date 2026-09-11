using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class PaymentNoteConfiguration : IEntityTypeConfiguration<PaymentNote>
{
    public void Configure(EntityTypeBuilder<PaymentNote> builder)
    {
        builder.ToTable("payment_notes", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.BillId).IsRequired();
        builder.Property(x => x.Amount).IsRequired();

        builder.Property(x => x.Method)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Date).IsRequired();

        builder.ConfigureSoftDelete();
        builder.ConfigureAudit();

        builder.HasIndex(x => x.BillId);
        builder.HasIndex(x => x.Method);
    }
}
