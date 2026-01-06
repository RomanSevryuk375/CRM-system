using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatusEntity>
{
    void IEntityTypeConfiguration<OrderStatusEntity>.Configure(EntityTypeBuilder<OrderStatusEntity> builder)
    {

        builder.ToTable("order_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(os => os.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.Pending,
                Name = "В ожидании",
            },
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.Accepted,
                Name = "Принят",
            },
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.Diagnostics,
                Name = "Диагнстика",
            },
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.Closed,
                Name = "Закрыт",
            },
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.InProgress,
                Name = "В работе",
            },
            new OrderStatusEntity
            {
                Id = (int)OrderStatusEnum.Completed,
                Name = "Завершен",
            }
            );

    }
}
