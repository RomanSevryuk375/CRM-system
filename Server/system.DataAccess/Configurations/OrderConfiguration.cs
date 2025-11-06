using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    void IEntityTypeConfiguration<OrderEntity>.Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("work_orders");

        builder.HasKey(x => x.Id);

        builder.Property(o => o.Id)
            .HasColumnName("work_order_id")
            .IsRequired();

        builder.Property(o => o.StatusId)
            .HasColumnName("work_order_status_id")
            .IsRequired();

        builder.Property(o => o.CarId)
            .HasColumnName("work_order_car_id")
            .IsRequired();

        builder.Property(o => o.Date)
            .HasColumnName("work_order_creation_date")
            .IsRequired();

        builder.Property(o => o.Priority)
            .HasColumnName("work_order_priority")
            .IsRequired();

        builder.HasOne(o => o.Car)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
