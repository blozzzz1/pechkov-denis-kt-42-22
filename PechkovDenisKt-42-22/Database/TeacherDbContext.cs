using Microsoft.EntityFrameworkCore;
using PechkovDenisKt_42_22.Models;
using PechkovDenisKt_42_22.Database.Configurations;


namespace PechkovDenisKt_42_22.Database
{
    public class TeacherDbContext : DbContext
    {

        public DbSet<Department> Departments { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Load> Loads { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new LoadConfiguration());

        }

        public TeacherDbContext(DbContextOptions<TeacherDbContext> optionts) : base(optionts)
        { }

    }
}