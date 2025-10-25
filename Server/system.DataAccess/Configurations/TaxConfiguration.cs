using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class TaxConfiguration : IEntityTypeConfiguration<TaxEntity>
{
    void IEntityTypeConfiguration<TaxEntity>.Configure(EntityTypeBuilder<TaxEntity> builder)
    {
        builder.ToTable("taxes");

        builder.HasKey(x => x.Id);

        builder.Property(t => t.Id)
            .HasColumnName("tax_id")
            .IsRequired();

        builder.Property(t => t.Name) 
            .HasColumnName("tax_name")
            .IsRequired();

        builder.Property(t => t.Rate)
            .HasColumnName("tax_rate")
            .IsRequired();

        builder.Property(t => t.Type)
            .HasColumnName("tax_type")
            .IsRequired();
    }
}
