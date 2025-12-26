using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<SpecializationEntity>
{
    void IEntityTypeConfiguration<SpecializationEntity>.Configure(EntityTypeBuilder<SpecializationEntity> builder)
    {

        builder.ToTable("specializations");

        builder.HasKey(x => x.Id);

        builder.Property(sp => sp.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.Mechanic,
                Name = "автомеханик",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.EngineTechnician,
                Name = "моторист",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.TransmissionTechnician,
                Name = "специалист по коробкам передач",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.SuspensionTechnician,
                Name = "ходовик",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.BrakeTechnician,
                Name = "специалист по тормозным системам",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.ElectricalTechnician,
                Name = "автоэлектрик",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.DiagnosticTechnician,
                Name = "диагност",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.BodyRepairTechnician,
                Name = "кузовщик",
            },
            new SpecializationEntity
            {
                Id = (int)SpecializationEnum.PainterTechnician,
                Name = "маляр",
            });

    }
}
