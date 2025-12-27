using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ExpenseTypeConfiguration : IEntityTypeConfiguration<ExpenseTypeEntity>
{
    void IEntityTypeConfiguration<ExpenseTypeEntity>.Configure(EntityTypeBuilder<ExpenseTypeEntity> builder)
    {
        builder.ToTable("expense_types");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.HasData(
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.PartsAndMaterials,
                Name = "Запчасти и материалы"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.RentAndUtilities,
                Name = "Аренда и коммунальные услуги"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.ToolsAndEquipment,
                Name = "Оборудование и инструмент"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.ITAndCommunication,
                Name = "IT и связь"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.Marketing,
                Name = "Маркетинг и реклама"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.DocumentationAndLicenses,
                Name = "Документация и лицензии"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.LogisticsAndTransport,
                Name = "Логистика и транспорт"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.OfficeAndSupplies,
                Name = "Офисные и хозяйственные расходы"
            },
            new ExpenseTypeEntity
            {
                Id = (int)ExpenseTypeEnum.FinancialCharges,
                Name = "Финансовые расходы"
            });
                
    }
}
