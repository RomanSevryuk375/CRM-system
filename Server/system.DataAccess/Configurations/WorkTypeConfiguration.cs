using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkTypeConfiguration : IEntityTypeConfiguration<WorkTypeEntity>
{
    void IEntityTypeConfiguration<WorkTypeEntity>.Configure(EntityTypeBuilder<WorkTypeEntity> builder)
    {
        builder.ToTable("catalog_of_works");

        builder.HasKey(x => x.Id);

        builder.Property(wt => wt.Id)
            .HasColumnName("job_id")
            .IsRequired();

        builder.Property(wt => wt.Title)
            .HasColumnName("title_of_work")
            .IsRequired();

        builder.Property(wt => wt.Category)
            .HasColumnName("catalog_of_works_category")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(wt => wt.Description)
            .HasColumnName("catalog_of_works_description")
            .IsRequired();

        builder.Property(wt => wt.StandardTime)
            .HasColumnName("catalog_of_works_standard_time")
            .IsRequired();
    }
}
