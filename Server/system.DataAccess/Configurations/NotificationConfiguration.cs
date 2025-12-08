using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<NotificationEntity>
{
    void IEntityTypeConfiguration<NotificationEntity>.Configure(EntityTypeBuilder<NotificationEntity> builder)
    {

        builder.ToTable("notifications");

        builder.HasKey(x => x.Id);

        builder.Property(n => n.ClientId)
            .IsRequired();

        builder.Property(n => n.CarId)
            .IsRequired();

        builder.Property(n => n.StatusId)
            .IsRequired();

        builder.Property(n => n.Type)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(n => n.Message)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(n => n.SendAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.HasOne(n => n.Client)
            .WithMany(no => no.Notifications)
            .HasForeignKey(n => n.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Car)
            .WithMany(c => c.Notifications)
            .HasForeignKey(n => n.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(n => n.Status)
            .WithMany(s => s.Notifications)
            .HasForeignKey(n => n.StatusId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
