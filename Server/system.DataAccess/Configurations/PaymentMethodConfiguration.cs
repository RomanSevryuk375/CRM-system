using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    void IEntityTypeConfiguration<PaymentMethodEntity>.Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("payment_methods");

        builder.HasKey(x => x.Id);

        builder.Property(os => os.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new PaymentMethodEntity 
            { 
                Id = (int)PaymentMethodEnum.Card,
                Name = "Картой"
            },
            new PaymentMethodEntity
            {
                Id = (int)PaymentMethodEnum.Cash,
                Name = "Наличными"
            },
            new PaymentMethodEntity
            {
                Id = (int)PaymentMethodEnum.Erip,
                Name = "ЕРИП"
            });
    }
}
