using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RegistrationNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Course).IsRequired().HasMaxLength(100);
                entity.Property(e => e.YearOfStudy).IsRequired();

                // Create unique index for Registration Number
                entity.HasIndex(e => e.RegistrationNumber).IsUnique();
            });
        }
    }
}