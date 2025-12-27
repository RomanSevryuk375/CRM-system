
using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class OrderPriorityConfiguration : IEntityTypeConfiguration<OrderPriorityEntity>
{
    void IEntityTypeConfiguration<OrderPriorityEntity>.Configure(EntityTypeBuilder<OrderPriorityEntity> builder)
    {
        builder.ToTable("order_priorities");

        builder.HasKey(x => x.Id);

        builder.Property(os => os.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new OrderPriorityEntity
            {
                Id = (int)OrderPriorityEnum.Low,
                Name = "Низкий"
            },
            new OrderPriorityEntity
            {
                Id = (int)OrderPriorityEnum.Medium,
                Name = "Обычый"
            },
            new OrderPriorityEntity
            {
                Id = (int)OrderPriorityEnum.High,
                Name = "Высокий"
            });

    }
}
