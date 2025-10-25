using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    void IEntityTypeConfiguration<WorkEntity>.Configure(EntityTypeBuilder<WorkEntity> builder)
    {
        builder.ToTable("work_in_order");

        builder.HasKey(x => x.Id);

        builder.Property(w => w.Id)
            .HasColumnName("work_note_id")
            .IsRequired();

        builder.Property(w => w.OrderId)
            .HasColumnName("work_order_id")
            .IsRequired();

        builder.Property(w => w.JobId)
            .HasColumnName("work_in_order_job_id")
            .IsRequired();

        builder.Property(w => w.WorkerId)
            .HasColumnName("work_in_order_worker_id")
            .IsRequired();

        builder.Property(w => w.TimeSpent)
            .HasColumnName("work_in_order_time_spent")
            .IsRequired();

        builder.Property(w => w.StatusId)
            .HasColumnName("work_in_order_status_id")
            .IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Status)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.StatusId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wt => wt.WorkType)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wo => wo.Worker)
            .WithMany(w => w.Works)
            .HasForeignKey(w => w.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
