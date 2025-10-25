using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class Specialization : IEntityTypeConfiguration<SpecializationEntity>
{
    void IEntityTypeConfiguration<SpecializationEntity>.Configure(EntityTypeBuilder<SpecializationEntity> builder)
    {
        builder.ToTable("specializations");

        builder.HasKey(x => x.Id);

        builder.Property(sp => sp.Id)
            .HasColumnName("specialization_id")
            .IsRequired();

        builder.Property(sp => sp.Name)
            .HasColumnName("specialization_name")
            .IsRequired();
    }
}
