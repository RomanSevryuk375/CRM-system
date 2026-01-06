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

        builder.Property(o => o.StatusId)
            .IsRequired();

        builder.Property(o => o.CarId)
            .IsRequired();

        builder.Property(o => o.Date)
            .IsRequired();

        builder.Property(o => o.PriorityId)
            .IsRequired();

        builder.HasOne(o => o.Car)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.OrderPriority)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.PriorityId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
