using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class CarStatusConfiguration : IEntityTypeConfiguration<CarStatusEntity>
{
    void IEntityTypeConfiguration<CarStatusEntity>.Configure(EntityTypeBuilder<CarStatusEntity> builder)
    {

        builder.ToTable("car_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(cs => cs.Name) 
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new CarStatusEntity
            {
                Id = (int)CarStatusEnum.AtWork,
                Name = "В работе",
            },
            new CarStatusEntity
            {
                Id = (int)CarStatusEnum.NotAtWork,
                Name = "Не в работе",
            });

    }
}
