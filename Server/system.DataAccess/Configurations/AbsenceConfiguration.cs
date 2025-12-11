using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRMSystem.DataAccess.Configurations;

public class AbsenceConfiguration : IEntityTypeConfiguration<AbsenceEntity>
{
    void IEntityTypeConfiguration<AbsenceEntity>.Configure(EntityTypeBuilder<AbsenceEntity> builder)
    {

        builder.ToTable("absences");

        builder.HasKey(x => x.Id);

        builder.Property(a => a.WorkerId)
            .IsRequired();

        builder.Property(a => a.TypeId)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(a => a.StartDate)
            .IsRequired();

        builder.Property(a => a.EndDate)
            .IsRequired(false);

        builder.HasOne(a => a.Worker)
            .WithMany(w => w.Absences)
            .HasForeignKey(a => a.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.AbsenceType)
            .WithMany(at => at.Absences)
            .HasForeignKey(a => a.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
