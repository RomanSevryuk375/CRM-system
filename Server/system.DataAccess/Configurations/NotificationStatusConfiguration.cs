using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class NotificationStatusConfiguration : IEntityTypeConfiguration<NotificationStatusEntity>
{
    void IEntityTypeConfiguration<NotificationStatusEntity>.Configure(EntityTypeBuilder<NotificationStatusEntity> builder)
    {

        builder.ToTable("notification_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(ns => ns.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new BillStatusEntity
            {
                Id = (int)NotificationStatusEnum.Sent,
                Name = "Отправлено",
            },
            new BillStatusEntity
            {
                Id = (int)NotificationStatusEnum.Read,
                Name = "Прочитано",
            }

        );

    }
}
