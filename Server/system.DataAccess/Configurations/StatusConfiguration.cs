using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

internal class StatusConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    void IEntityTypeConfiguration<StatusEntity>.Configure(EntityTypeBuilder<StatusEntity> builder)
    {
        builder.ToTable("directory_of_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.Id)
            .HasColumnName("status_id")
            .IsRequired();

        builder.Property(s => s.Name)
            .HasColumnName("status_name")
            .IsRequired();

        builder.Property(s => s.Description)
            .HasColumnName("status_description")
            .IsRequired();
    }
}
