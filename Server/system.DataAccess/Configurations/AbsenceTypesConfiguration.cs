using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class AbsenceTypesConfiguration : IEntityTypeConfiguration<AbsenceTypeEntity>
{
    void IEntityTypeConfiguration<AbsenceTypeEntity>.Configure(EntityTypeBuilder<AbsenceTypeEntity> builder)
    {

        builder.ToTable("absence_types");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new AbsenceTypeEntity
            {
                Id = (int)AbsenceTypeEnum.SickLeave,
                Name = "Больничный"
            },
            new AbsenceTypeEntity
            {
                Id = (int)AbsenceTypeEnum.Absenteeism,
                Name = "Прогул"
            },
            new AbsenceTypeEntity
            {
                Id = (int)AbsenceTypeEnum.Vacation,
                Name = "Отпуск"
            },
            new AbsenceTypeEntity
            {
                Id = (int)AbsenceTypeEnum.TimeOff,
                Name = "Отгул"
            });
    }
}
