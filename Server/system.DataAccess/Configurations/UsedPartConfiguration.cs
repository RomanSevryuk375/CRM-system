using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class UsedPartConfiguration : IEntityTypeConfiguration<PartSetEntity>
{
    void IEntityTypeConfiguration<PartSetEntity>.Configure(EntityTypeBuilder<PartSetEntity> builder)
    {
        builder.ToTable("used_parts");

        builder.HasKey(x => x.Id);

        builder.Property(up => up.Id)
            .HasColumnName("used_part_id")
            .IsRequired();

        builder.Property(up => up.OrderId)
            .HasColumnName("used_parts_order_id")
            .IsRequired();

        builder.Property(up => up.SupplierId)
            .HasColumnName("used_parts_supplier_id")
            .IsRequired();

        builder.Property(up => up.Name)
            .HasColumnName("used_part_name")
            .IsRequired();

        builder.Property(up => up.Article)
            .HasColumnName("used_part_article")
            .IsRequired();

        builder.Property(up => up.Quantity)
            .HasColumnName("used_part_quantity")
            .IsRequired();

        builder.Property(up => up.UnitPrice)
            .HasColumnName("used_part_unit_price")
            .IsRequired();

        builder.Property(up => up.Sum)
            .HasColumnName("used_part_total_sum")
            .ValueGeneratedOnAddOrUpdate()
            .IsRequired(false);

        builder.HasOne(o => o.Order)
            .WithMany(up => up.UsedParts)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Supplier) 
            .WithMany(up => up.UsedParts)
            .HasForeignKey(up => up.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
