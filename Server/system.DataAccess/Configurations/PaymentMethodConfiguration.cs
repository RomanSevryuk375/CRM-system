using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    void IEntityTypeConfiguration<PaymentMethodEntity>.Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("payment_methods");

        builder.HasKey(x => x.Id);

        builder.Property(os => os.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasData(
            new PaymentMethodEntity 
            { 
                Id = (int)PaymentMethodEnum.card,
                Name = "Картой"
            },
            new PaymentMethodEntity
            {
                Id = (int)PaymentMethodEnum.cash,
                Name = "Наличными"
            },
            new PaymentMethodEntity
            {
                Id = (int)PaymentMethodEnum.ERIP,
                Name = "ЕРИП"
            });
    }
}
