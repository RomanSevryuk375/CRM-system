using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<PositionEntity>
{
    void IEntityTypeConfiguration<PositionEntity>.Configure(EntityTypeBuilder<PositionEntity> builder)
    {

        builder.ToTable("positions");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.PartId)
            .IsRequired();

        builder.Property(p => p.CellId)
            .IsRequired();

        builder.Property(p => p.PurchasePrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(p => p.SellingPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(p => p.Quantity)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(p => p.Part)
            .WithMany(pa => pa.Positions)
            .HasForeignKey(p => p.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.StorageCell)
            .WithMany(s => s.Positions)
            .HasForeignKey(p => p.CellId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
