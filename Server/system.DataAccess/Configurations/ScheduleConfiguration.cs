using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ScheduleConfiguration : IEntityTypeConfiguration<ScheduleEntity>
{
    void IEntityTypeConfiguration<ScheduleEntity>.Configure(EntityTypeBuilder<ScheduleEntity> builder)
    {

        builder.ToTable("schedules");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.WorkerId)
            .IsRequired();

        builder.Property(s => s.ShiftId)
            .IsRequired();

        builder.Property(s => s.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.HasOne(s => s.Worker)
            .WithMany(w => w.Schedules)
            .HasForeignKey(s => s.WorkerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Shift)
            .WithMany(si => si.Schedules)
            .HasForeignKey(s => s.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
