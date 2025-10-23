using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

internal class RepairHistoryConfiguration : IEntityTypeConfiguration<RepairHistoryEntity>
{
    void IEntityTypeConfiguration<RepairHistoryEntity>.Configure(EntityTypeBuilder<RepairHistoryEntity> builder)
    {
        builder.ToTable("service_history");

        builder.HasKey(x => x.Id);

        builder.Property(r => r.Id)
            .HasColumnName("service_history_id")
            .IsRequired();

        builder.Property(r => r.CarId)
            .HasColumnName("service_history_car_id")
            .IsRequired();

        builder.Property(r => r.OrderId)
            .HasColumnName("service_history_work_order_id")
            .IsRequired();

        builder.Property(r => r.ServiceSum)
            .HasColumnName("service_history_sum")
            .IsRequired();

        builder.Property(r => r.WorkDate)
            .HasColumnName("service_history_work_complite_date")
            .IsRequired();

        builder.HasOne(c => c.Car)
            .WithMany(car => car.histories)
            .HasForeignKey(c => c.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // order 
    }
}
