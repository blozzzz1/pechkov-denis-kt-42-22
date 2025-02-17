using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PechkovDenisKt_42_22.Models;

namespace PechkovDenisKt_42_22.Database.Configurations
{
    public class LoadConfiguration : IEntityTypeConfiguration<Load>
    {
        public void Configure(EntityTypeBuilder<Load> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Hours)
                .IsRequired();

            builder.HasOne(l => l.Teacher)
                .WithMany()
                .HasForeignKey("TeacherId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(l => l.Discipline)
                .WithMany()
                .HasForeignKey("DisciplineId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}