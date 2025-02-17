using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PechkovDenisKt_42_22.Models;

namespace PechkovDenisKt_42_22.Data.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(t => t.Degree)
                .WithMany()
                .HasForeignKey(t => t.DegreeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Position)
                .WithMany()
                .HasForeignKey(t => t.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Disciplines)
                .WithOne()
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}