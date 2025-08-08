using Microsoft.EntityFrameworkCore;
using Register.Models;
//using System.Data.Entity;

namespace Register.Data
{
    public class RegisterContext : DbContext
    {
        public RegisterContext(DbContextOptions<RegisterContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<HighSchool> HighSchools { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Aspiration> Aspirations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Province)
                .WithMany(p => p.Student) 
                .HasForeignKey(s => s.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
