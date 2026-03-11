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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.RegistrationNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Course).IsRequired().HasMaxLength(100);
                entity.Property(e => e.YearOfStudy).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");

                // Create unique index for Registration Number
                entity.HasIndex(e => e.RegistrationNumber).IsUnique();
                // Create unique index for Email
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Role).HasMaxLength(20).HasDefaultValue("User");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("datetime('now')");

                // Create unique indexes
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Seed default admin user (password: admin123)
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FullName = "System Administrator",
                Email = "admin@studentreg.com",
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.Now
            });
        }
    }
}