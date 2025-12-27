using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class BillStatusConfiguretion : IEntityTypeConfiguration<BillStatusEntity>
{
    void IEntityTypeConfiguration<BillStatusEntity>.Configure(EntityTypeBuilder<BillStatusEntity> builder)
    {

        builder.ToTable("bill_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(bs => bs.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new BillStatusEntity
            {
                Id = (int)BillStatusEnum.Paid,
                Name = "Оплачен",
            },
            new BillStatusEntity
            {
                Id = (int)BillStatusEnum.Unpaid,
                Name = "Не оплачен",
            },
            new BillStatusEntity
            {
                Id = (int)BillStatusEnum.PartiallyPaid,
                Name = "Частично оплачен",
            }

        );
    }
}
