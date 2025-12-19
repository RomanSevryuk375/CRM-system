using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkInOrderConfiguration : IEntityTypeConfiguration<WorkInOrderEntity>
{
    void IEntityTypeConfiguration<WorkInOrderEntity>.Configure(EntityTypeBuilder<WorkInOrderEntity> builder)
    {
        
        builder.ToTable("works_in_order");

        builder.HasKey(x => x.Id);

        builder.Property(w => w.OrderId)
            .IsRequired();

        builder.Property(w => w.JobId)
            .IsRequired();

        builder.Property(w => w.WorkerId)
            .IsRequired();

        builder.Property(w => w.TimeSpent)
            .HasColumnType("decimal(4, 2)")
            .IsRequired();

        builder.Property(w => w.StatusId)
            //.HasConversion<int>()
            .IsRequired();

        builder.HasOne(w => w.Order)
            .WithMany(o => o.WorksInOrder)
            .HasForeignKey(w => w.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Work)
            .WithMany(wt => wt.WorksInOrder)
            .HasForeignKey(w => w.JobId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.Worker)
            .WithMany(wo => wo.WorksInOrder)
            .HasForeignKey(w => w.WorkerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.WorkInOrderStatus)
            .WithMany(wos => wos.Works)
            .HasForeignKey(w => w.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
