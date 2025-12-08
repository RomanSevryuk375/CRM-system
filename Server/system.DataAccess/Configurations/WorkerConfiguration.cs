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

        builder.Property(wo => wo.UserId)          
            .IsRequired();

        builder.Property(wo => wo.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(wo => wo.Surname)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(wo => wo.HourlyRate)
            .HasColumnType("decimal (8, 2)")
            .IsRequired();

        builder.Property(wo => wo.PhoneNumber)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(wo => wo.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithOne(wo => wo.Worker)
            .HasForeignKey<WorkerEntity>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
