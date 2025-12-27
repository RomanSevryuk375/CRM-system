using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkInOrderStatusConfiguration : IEntityTypeConfiguration<WorkInOrderStatusEntity>
{
    void IEntityTypeConfiguration<WorkInOrderStatusEntity>.Configure(EntityTypeBuilder<WorkInOrderStatusEntity> builder)
    {

        builder.ToTable("work_in_order_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new WorkInOrderStatusEntity
            {
                Id = (int)WorkStatusEnum.InProgress,
                Name = "В работе",
            },
            new WorkInOrderStatusEntity
            {
                Id = (int)WorkStatusEnum.Pending,
                Name = "В ожидании",
            },
            new WorkInOrderStatusEntity
            {
                Id = (int)WorkStatusEnum.Completed,
                Name = "завершен",
            });

    }
}
