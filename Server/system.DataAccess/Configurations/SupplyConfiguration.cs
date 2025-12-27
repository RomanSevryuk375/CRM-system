using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class SupplyConfiguration : IEntityTypeConfiguration<SupplyEntity>
{
    void IEntityTypeConfiguration<SupplyEntity>.Configure(EntityTypeBuilder<SupplyEntity> builder)
    {

        builder.ToTable("supplies");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.SupplierId)
            .IsRequired();

        builder.Property(s => s.Date)
            .IsRequired();

        builder.HasOne(s => s.Supplier)
            .WithMany(su => su.Supplies)
            .HasForeignKey(s => s.SupplierId)
            .OnDelete(DeleteBehavior.Restrict); 

    }
}
