namespace Calisthenix.Server.Data
{
    using System.Reflection;
    using Calisthenix.Server.Models;
    using Microsoft.EntityFrameworkCore;

    public class CalisthenixDbContext : DbContext
    {
        public CalisthenixDbContext(DbContextOptions<CalisthenixDbContext> options) : base(options) 
        {
        
        }

        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
