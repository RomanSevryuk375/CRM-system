using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationTypeEntity>
{
    void IEntityTypeConfiguration<NotificationTypeEntity>.Configure(EntityTypeBuilder<NotificationTypeEntity> builder)
    {

        builder.ToTable("notification_types");

        builder.HasKey(x => x.Id);

        builder.Property(ns => ns.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.OrderWorkflow,
                Name = "Работа с заказами"
            },
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.PaymentsFinance,
                Name = "Платежи и финансы"
            },
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.InventoryParts,
                Name = "Запчасти и склад"
            },
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.MaintenanceScheduling,
                Name = "Планирование и обслуживание"
            },
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.System,
                Name = "Системные уведомления"
            },
            new NotificationTypeEntity
            {
                Id = (int)NotificationTypeEnum.Client,
                Name = "Коммуникация с клиентами"
            }
        );

    }
}
