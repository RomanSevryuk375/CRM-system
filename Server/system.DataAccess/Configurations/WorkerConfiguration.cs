using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkerConfiguration : IEntityTypeConfiguration<WorkerEntity>
{
    void IEntityTypeConfiguration<WorkerEntity>.Configure(EntityTypeBuilder<WorkerEntity> builder)
    {
        builder.ToTable("workers");

        builder.HasKey(x => x.Id);

        builder.Property(wo => wo.Id)
            .HasColumnName("worker_id")
            .IsRequired();

        builder.Property(wo => wo.UserId)
            .HasColumnName("worker_user_id")
            .IsRequired();

        builder.Property(wo => wo.SpecializationId)
            .HasColumnName("worker_specialization_id")
            .IsRequired();

        builder.Property(wo => wo.Name)
            .HasColumnName("worker_name")
            .IsRequired();

        builder.Property(wo => wo.Surname)
            .HasColumnName("worker_surname")
            .IsRequired();

        builder.Property(wo => wo.HourlyRate)
            .HasColumnName("worker_hourly_rate")
            .IsRequired();

        builder.Property(wo => wo.PhoneNumber)
            .HasColumnName("worker_phone")
            .IsRequired();

        builder.Property(wo => wo.Email)
            .HasColumnName("worker_email")
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithOne(wo => wo.Worker)
            .HasForeignKey<WorkerEntity>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Specialization)
            .WithMany(wo => wo.Workers)
            .HasForeignKey(wo => wo.SpecializationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
