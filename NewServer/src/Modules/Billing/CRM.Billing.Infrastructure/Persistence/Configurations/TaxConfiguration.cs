using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class TaxConfiguration : IEntityTypeConfiguration<Tax>
{
    public void Configure(EntityTypeBuilder<Tax> builder)
    {
        builder.ToTable("taxes", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new TaxId(value))
            .IsRequired();

        builder.Property(x => x.Rate)
            .HasConversion(
                vo => vo.Value,
                dbVal => TaxRate.Create(dbVal).Value)
            .HasPrecision(5, 4)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
                vo => vo.Value,
                dbVal => Name.Create(dbVal).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(x => x.Version).IsConcurrencyToken();

        builder.HasIndex(x => x.Type);
    }
}
