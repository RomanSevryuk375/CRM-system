using CRMSystem.Core.Constants;
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
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.Property(t => t.Rate)
            .HasColumnType("decimal(2, 2)")
            .IsRequired();

        builder.Property(t => t.TypeId)
            .IsRequired();

        builder.HasOne(t => t.TaxType)
            .WithMany(tt => tt.Taxes)
            .HasForeignKey(t => t.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
