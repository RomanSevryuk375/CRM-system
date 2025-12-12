using CRMSystem.Core.Constants;
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
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(wo => wo.Surname)
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(wo => wo.HourlyRate)
            .HasColumnType("decimal (8, 2)")
            .IsRequired();

        builder.Property(wo => wo.PhoneNumber)
            .HasMaxLength(ValidationConstants.MAX_PHONE_LENGTH)
            .IsRequired();

        builder.Property(wo => wo.Email)
            .HasMaxLength(ValidationConstants.MAX_EMAIL_LENGTH)
            .IsRequired();

        builder.HasOne(u => u.User)
            .WithOne(wo => wo.Worker)
            .HasForeignKey<WorkerEntity>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(wo => wo.Email)
            .IsUnique();

        builder.HasIndex(wo => wo.PhoneNumber)
            .IsUnique();

    }
}
