using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PechkovDenisKt_42_22.Models;

namespace PechkovDenisKt_42_22.Database.Configurations
{
    public class LoadConfiguration : IEntityTypeConfiguration<Load>
    {
        private const string TableName = "Loads";

        public void Configure(EntityTypeBuilder<Load> builder)
        {
            builder.ToTable(TableName); 

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Hours)
                .IsRequired();

            // Связь с учителем
            builder.HasOne(l => l.Teacher)
                .WithMany(t => t.Loads) 
                .HasForeignKey(l => l.TeacherId)
                .OnDelete(DeleteBehavior.Cascade); 

            // Связь с дисциплиной
            builder.HasOne(l => l.Discipline)
                .WithMany(d => d.Loads) 
                .HasForeignKey(l => l.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade); // Измените на Cascade, если хотите удалять нагрузки при удалении дисциплины
        }
    }
}