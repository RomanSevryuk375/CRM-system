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

        builder.Property(t => t.Name) 
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(t => t.Rate)
            .HasColumnType("decimal(2, 2)")
            .IsRequired();

        builder.Property(t => t.Type)
            .HasMaxLength(128)
            .IsRequired();

    }
}
