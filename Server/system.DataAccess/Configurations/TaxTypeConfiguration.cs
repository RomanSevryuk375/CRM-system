using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class TaxTypeConfiguration : IEntityTypeConfiguration<TaxTypeEntity>
{
    void IEntityTypeConfiguration<TaxTypeEntity>.Configure(EntityTypeBuilder<TaxTypeEntity> builder)
    {
        builder.ToTable("tax_types");

        builder.HasKey(x => x.Id);

        builder.Property(os => os.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new TaxTypeEntity { Id = (int)TaxTypeEnum.CorporateIncomeTax, Name = "Налог на прибыль" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.ValueAddedTax, Name = "НДС" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.PropertyTax, Name = "Налог на недвижимость" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.LandTax, Name = "Земельный налог" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.SocialSecurityContributions, Name = "Социальные взносы" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.EnvironmentalTax, Name = "Экологический налог" },
            new TaxTypeEntity { Id = (int)TaxTypeEnum.LocalFees, Name = "Местные сборы" }
            );

    }
}
