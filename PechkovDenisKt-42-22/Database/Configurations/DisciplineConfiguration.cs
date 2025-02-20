using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Models;

namespace PechkovDenisKt_42_22.Database.Configurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        private const string TableName = "Disciplines";

        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable(TableName); // Убедитесь, что таблица называется "Disciplines"

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired();

            // Связь с нагрузками
            builder.HasMany(d => d.Loads)
                .WithOne(l => l.Discipline)
                .HasForeignKey(l => l.DisciplineId) // Указываем внешний ключ
                .OnDelete(DeleteBehavior.Cascade); // Удаление нагрузки при удалении дисциплины
        }
    }
}
