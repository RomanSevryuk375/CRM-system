using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class SupplySetConfiguration : IEntityTypeConfiguration<SupplySetEntity>
{
    void IEntityTypeConfiguration<SupplySetEntity>.Configure(EntityTypeBuilder<SupplySetEntity> builder)
    {

        builder.ToTable("supply_sets");

        builder.HasKey(x => x.Id);

        builder.Property(ss => ss.SupplyId)
            .IsRequired();

        builder.Property(ss => ss.PositionId)
            .IsRequired();
        
        builder.Property(ss => ss.Quantity)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(ss => ss.PurchasePrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(ss => ss.Supply)
            .WithMany(s => s.SupplySets)
            .HasForeignKey(s => s.SupplyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ss => ss.Position)
            .WithMany(p => p.SupplySets)
            .HasForeignKey(ss => ss.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
                
    }
}
