using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
{
    public void Configure(EntityTypeBuilder<PriceList> builder)
    {
        builder.ToTable("price_lists", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new PriceListId(value))
            .IsRequired();

        builder.Property(x => x.BaseHourlyRate)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
                vo => vo.Value,
                dbVal => Name.Create(dbVal).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(x => x.ValidFrom).IsRequired();
        builder.Property(x => x.ValidTo).IsRequired(false);
        builder.Property(x => x.IsDefault).IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.IsDefault);
        builder.HasIndex(x => x.ValidFrom);
        builder.HasIndex(x => x.ValidTo);

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(x => x.PriceListId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
               .FindNavigation(nameof(PriceList.Items))!
               .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
