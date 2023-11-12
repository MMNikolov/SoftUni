using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {
        private const string ConnectionString =
        "Server=DESKTOP-PP7HKGS\\SQLEXPRESS;Database=FootballBetting;Integrated Security=True";

        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseId);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.StudentId);

            modelBuilder.Entity<Resource>()
                .HasKey(r => r.ResourceId);

            modelBuilder.Entity<Homework>()
                .HasKey(h => h.HomeworkId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                    .HasMaxLength(80)
                    .IsUnicode(true);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                    .IsUnicode(true)
                    .IsRequired(false);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

            modelBuilder.Entity<Resource>()
                .Property(r => r.Url)
                    .IsUnicode(false);

            modelBuilder.Entity<Homework>()
                .Property(h => h.Content)
                    .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(s => s.Name)
                    .HasMaxLength(100)
                    .IsUnicode(true);

            modelBuilder.Entity<Student>()
                .Property(s => s.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(false);

            modelBuilder.Entity<Student>()
                .Property(s => s.Birthday)
                    .IsRequired(false);

            modelBuilder.Entity<StudentCourse>()
                .HasKey(ps => new
                {
                    ps.StudentId,
                    ps.CourseId
                });

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(r => r.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Student)
                    .WithMany(s => s.Courses)
                    .HasForeignKey(r => r.CourseId);

        }
    }
}