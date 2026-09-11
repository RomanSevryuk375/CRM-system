using CRM.Billing.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class PriceListConfiguration : IEntityTypeConfiguration<PriceList>
{
    public void Configure(EntityTypeBuilder<PriceList> builder)
    {
        builder.ToTable("price_lists", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.BaseHourlyRate).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.ValidFrom).IsRequired();
        builder.Property(x => x.ValidTo).IsRequired(false);
        builder.Property(x => x.IsDefault).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.IsDefault);
        builder.HasIndex(x => x.ValidFrom);
        builder.HasIndex(x => x.ValidTo);

        builder.HasMany(x => x.Items)
               .WithOne()
               .HasForeignKey(x => x.PriceListId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(x => x.Items)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
