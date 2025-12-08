using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class NotificationStatusConfiguration : IEntityTypeConfiguration<NotificationStatusEntity>
{
    void IEntityTypeConfiguration<NotificationStatusEntity>.Configure(EntityTypeBuilder<NotificationStatusEntity> builder)
    {

        builder.ToTable("notification_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(ns => ns.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasData(
            new BillStatusEntity
            {
                Id = (int)NotificationStatuseEnum.Sent,
                Name = "Отправлено",
            },
            new BillStatusEntity
            {
                Id = (int)NotificationStatuseEnum.Read,
                Name = "Прочитано",
            }

        );

    }
}
