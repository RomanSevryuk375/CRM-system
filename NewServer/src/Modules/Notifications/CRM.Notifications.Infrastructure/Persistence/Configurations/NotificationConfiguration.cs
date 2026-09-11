using CRM.Notifications.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Notifications.Infrastructure.Persistence.Configurations;

internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications", NotificationsDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.CustomerId).IsRequired();
        builder.Property(x => x.CarId).IsRequired(false);

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Message)
            .HasMaxLength(Notification.MaxMessageLength)
            .IsRequired();

        builder.Property(x => x.SendAt).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.CarId);
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.SendAt);
    }
}
