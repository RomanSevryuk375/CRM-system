using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class PriceListItemConfiguration : IEntityTypeConfiguration<PriceListItem>
{
    public void Configure(EntityTypeBuilder<PriceListItem> builder)
    {
        builder.ToTable("price_list_items", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new PriceListItemId(value))
            .IsRequired();

        builder.Property(x => x.PriceListId)
            .HasConversion(
                id => id.Id,
                value => new PriceListId(value))
            .IsRequired();

        builder.Property(x => x.JobId)
            .HasConversion(
                id => id.Id,
                value => new JobId(value))
            .IsRequired();

        builder.Property(x => x.FixedPrice)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.PriceListId);
        builder.HasIndex(x => x.JobId);
    }
}
